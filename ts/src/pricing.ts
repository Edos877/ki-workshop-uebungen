// Preisberechnung des Onlineshops (TypeScript-Variante der Übung 1).
//
// Regeln (alle Preise netto):
// - Kundenrabatt: gold 10 %, silver 5 %, standard 0 %
// - Gutschein WILLKOMMEN10: 10 € Nachlass ab 50 € Warenwert, zusätzlich zum Kundenrabatt
// - Gutschein SOMMER15: 15 %, nicht mit dem Kundenrabatt kombinierbar (der höhere gilt)
// - Versand DE: 4,95 €, kostenlos ab 50 € (nach Rabatt); AT/CH: 9,90 €, kostenlos ab 100 €
// - Umsatzsteuer: DE 19 %, AT 20 %, CH 0 %
// - Rundung auf 2 Nachkommastellen

export type Tier = "standard" | "silver" | "gold";
export interface Item { sku: string; price: number; qty: number }
export interface Order { tier: Tier; items: Item[]; coupon?: string; country: string }
export interface Result { ok: boolean; error?: string; subtotal: number; discount: number; shipping: number; tax: number; total: number }

const r2 = (n: number) => Math.round((n + Number.EPSILON) * 100) / 100;

export function calc(o: Order): Result {
  const fail = (e: string): Result => ({ ok: false, error: e, subtotal: 0, discount: 0, shipping: 0, tax: 0, total: 0 });
  if (!o.items || o.items.length == 0) return fail("Bestellung enthält keine Positionen");
  for (const i of o.items) {
    if (i.qty <= 0) return fail("Ungültige Menge bei " + i.sku);
    if (i.price < 0) return fail("Ungültiger Preis bei " + i.sku);
  }
  if (o.country != "DE" && o.country != "AT" && o.country != "CH") return fail("Lieferland nicht unterstützt");

  let s = 0;
  for (let x = 0; x < o.items.length; x++) s = s + o.items[x].price * o.items[x].qty;

  let d = 0;
  if (o.coupon) {
    const c = o.coupon.trim().toUpperCase();
    if (c == "WILLKOMMEN10") {
      if (s >= 50) d = 10;
      if (o.tier == "gold") d = d + s * 0.1;
      else if (o.tier == "silver") d = d + s * 0.05;
    } else if (c == "SOMMER15") {
      let t = 0;
      if (o.tier == "gold") t = s * 0.1;
      else if (o.tier == "silver") t = s * 0.05;
      const p = s * 0.15;
      d = p > t ? p : t;
    } else {
      if (o.tier == "gold") d = s * 0.1;
      else if (o.tier == "silver") d = s * 0.05;
    }
  } else {
    if (o.tier == "gold") d = s * 0.1;
    else if (o.tier == "silver") d = s * 0.05;
  }
  d = r2(d);

  const a = s - d;
  let sh = 0;
  if (o.country == "DE") { if (a >= 50) sh = 0; else sh = 4.95; }
  else { if (a >= 100) sh = 0; else sh = 9.9; }

  let rate = 0;
  if (o.country == "DE") rate = 0.19;
  if (o.country == "AT") rate = 0.2;
  const tax = r2((a + sh) * rate);
  return { ok: true, subtotal: r2(s), discount: d, shipping: sh, tax, total: r2(a + sh + tax) };
}
