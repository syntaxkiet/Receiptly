using Shared.Service.ReceiptParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiptly.Test.ReceiptProcessing.Resources
{
    internal class TestResourceHelper
    {
        public static string OcrResultOfReceiptSample1 { get; set; } = @"Hemköp Gallerian
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
        public static List<ReceiptItem> ExpectedReceiptItems { get; set; } = new();
        //ToDo
        //Remove hardcoded path and replace with path relative to this files location
        // string helperFileDirectory = Path.GetDirectoryName(new Uri(typeof(TestResourceHelper).Assembly.Location).LocalPath);
        public static string ReceiptSample1Path {get; set;} = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"ReceiptProcessing", "Resources", "ReceiptSample1.jpg");
        public static string TessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
    }
}
