namespace Shared.Sample;

public static class ReceiptSample
{
    public static string ReceiptString { get; set; } = @"Hemköp Gallerian
Tel : 016-131040
Öppettider
Mån-Fre 7-22, Lör-Sön 8-22

MJÖLK 33 1L 17,95
GODIS LV
0,330kg>+69, 50kr/kg 22,93
CARAMEL MILKSHAKE 11,00
JULMUST SOCKERFR1.4L 16,95
+PANT ENG PET >1L 2,00
LEMON LIME 16,00
+PANT ENG PET <=1L 1,00
POPCORNKÄRNOR 500G + 2st+17,95 35,90
MAGNUM WONDER 12,00

Totalt 8 varor
Totalt 135,73 SEK

Mottaget Kontokort 135,73
MasterCard RROKRKRARARAFAHB
KOP 135.73 SEK
Butik:++0237
Ref: 200262304826 Term: 20026230
TVR: 0000048001 AID: A0000000041010
2024-10-25 15:47:55 TSI: E800
Kontaktlös KA1 7 001 SWE 222575
Moms?s Moms Netto Brutto
12,00 14,54 121,19 135, 73
Poänggrundande belopp: 132,73
x = Ej Bonusgrundande varor: 3,00
Medlemsnummer: 9752299157404517

SPARA KVITTOT

Orgnr :556113-8826
välkommen åter
Du är väl bonuskund?
Om inte - säg till i kassan nästa gång
så löser vi det på 1 minut

Du betjänades av
Självcheckout Kassör

Kassa: 5/132 2024-10-25 15:47
";

    public static string AISeparatedString { get; set; } = @"- **Store**: Hemköp Gallerian  
- **Phone**: 016-131040  
- **Opening Hours**:  
  - **Mon-Fri**: 7-22  
  - **Sat-Sun**: 8-22  
- **Date/Time**: 2024-10-25, 15:47  
- **Payment Method**: Mastercard, 135.73 SEK  

### Purchased Items:
1. **Mjölk 3% (1L)**: 17.95 SEK  
2. **Godis lösvikt (0.330 kg @ 69.50 kr/kg)**: 22.93 SEK  
3. **Caramel Milkshake**: 11.00 SEK  
4. **Julmust sockerfri (1.4L)**: 16.95 SEK  
    - **Pant** (recycling deposit for bottles >1L): 2.00 SEK  
5. **Lemon Lime**: 16.00 SEK  
    - **Pant** (recycling deposit for bottles ≤1L): 1.00 SEK  
6. **Popcornkärnor (500g x 2)**: 35.90 SEK  
7. **Magnum Wonder**: 12.00 SEK  

### Totals:
- **Number of items**: 8  
- **Subtotal**: 135.73 SEK  
- **Payment Received**: 135.73 SEK  

### VAT Details:
- **VAT**: 12%  
- **Net (before VAT)**: 121.19 SEK  
- **Gross (incl. VAT)**: 135.73 SEK  ";
}