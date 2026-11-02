// Diese Tests halten das heutige Verhalten fest. Beim Refactoring müssen sie grün bleiben.
import { describe, expect, it } from "vitest";
import { calc, type Order } from "./pricing";

const order = (price: number, extra: Partial<Order> = {}): Order => ({
  tier: "standard", items: [{ sku: "A1", price, qty: 1 }], country: "DE", ...extra,
});

describe("calc", () => {
  it("lehnt leere Bestellungen ab", () => expect(calc(order(10, { items: [] })).ok).toBe(false));
  it("lehnt Menge 0 ab", () => expect(calc(order(10, { items: [{ sku: "A1", price: 10, qty: 0 }] })).error).toBe("Ungültige Menge bei A1"));
  it("lehnt unbekannte Länder ab", () => expect(calc(order(10, { country: "FR" })).ok).toBe(false));
  it("DE unter 50 € mit Versand", () => expect(calc(order(40)).total).toBe(53.49));
  it("DE ab 50 € versandkostenfrei", () => expect(calc(order(60)).shipping).toBe(0));
  it("Gold 10 %", () => expect(calc(order(100, { tier: "gold" })).total).toBe(107.1));
  it("Silver 5 %", () => expect(calc(order(100, { tier: "silver" })).total).toBe(113.05));
  it("WILLKOMMEN10 ab 50 €", () => expect(calc(order(60, { coupon: "WILLKOMMEN10" })).total).toBe(59.5));
  it("SOMMER15 schlägt Gold", () => expect(calc(order(100, { tier: "gold", coupon: " sommer15 " })).discount).toBe(15));
  it("Österreich", () => expect(calc(order(80, { country: "AT" })).total).toBe(107.88));
  it("Schweiz ohne Steuer", () => expect(calc(order(120, { country: "CH" })).total).toBe(120));
});
