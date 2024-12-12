using Shared.Models;
using Shared.Service.ReceiptParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Receiptly.Test.ReceiptProcessing.Resources
{
    internal class ReceiptProcessingTestResourceHelper
    {
        public static string OcrResultOfReceiptSample1 { get; set; } = @"Hemköp Gallerian\nTel : 016-131040\nÖppettider\nMån-Fre 7-22, Lör-Sön 8-22\n\nMJÖLK 33 1L 17,95\nGODIS LV\n0,330kg>+69, 50kr/kg 22,93\nCARAMEL MILKSHAKE 11,00\nJULMUST SOCKERFR1.4L 16,95\n+PANT ENG PET >1L 2,00\nLEMON LIME 16,00\n+PANT ENG PET <=1L 1,00\nPOPCORNKÄRNOR 500G + 2st+17,95 35,90\nMAGNUM WONDER 12,00\n\nTotalt 8 varor\nTotalt 135,73 SEK\n\nMottaget Kontokort 135,73\nMasterCard RROKRKRARARAFAHB\nKOP 135.73 SEK\nButik:++0237\nRef: 200262304826 Term: 20026230\nTVR: 0000048001 AID: A0000000041010\n2024-10-25 15:47:55 TSI: E800\nKontaktlös KA1 7 001 SWE 222575\nMoms?s Moms Netto Brutto\n12,00 14,54 121,19 135, 73\nPoänggrundande belopp: 132,73\nx = Ej Bonusgrundande varor: 3,00\nMedlemsnummer: 9752299157404517\n\nSPARA KVITTOT\n\nOrgnr :556113-8826\nvälkommen åter\nDu är väl bonuskund?\nOm inte - säg till i kassan nästa gång\nså löser vi det på 1 minut\n\nDu betjänades av\nSjälvcheckout Kassör\n\nKassa: 5/132 2024-10-25 15:47\n";
        public static List<Item> ExpectedReceiptItems { get; set; } = new();
        public static string ReceiptSample1Path { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReceiptProcessing", "Resources", "ReceiptSample1.jpg");
        public static string TessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
        public static ReceiptParseModel TestModel { get; } = new ReceiptParseModel()
        {
            Name = "Hemköp Kungsgatan Eskilstuna",
            ItemLinePattern = @"\b\d{1,4},\d{2}$",
            StorePattern = @"Hemköp",
            LineSeparatorPattern = @"\n",
            PuchaseDatePattern = @"(?<PurchaseDate>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})",
            DualLinePatterns = new List<string>() { @"\+PANT", @"\d{1,3}[,]\d{2}/kg", @"\d{1,3},\s?\d{2}kr/\w{1,3}" },
            TriggersAtNextIndexInsertsBeforeOnCurrentIndex = new() { { @"(?i)\+PANT(?!\S)", @"\b\d{1,4},\d{2}$" } },
            ExcludeLineFromItems = new List<string>() {@"(?i)\b bonusgrundande\b", @"(?i)\bkontokort\b", @"(?i)\bpoänggrundande\b"}
        };
        public static List<string> ExpectedSeparatedCombinedStringsList { get; set; } = new()
{
    "Hemköp Gallerian",
    "Tel : 016-131040",
    "Öppettider",
    "Mån-Fre 7-22, Lör-Sön 8-22",
    "MJÖLK 33 1L 17,95",
    "GODIS LV 0,330kg>+69, 50kr/kg 22,93",
    "CARAMEL MILKSHAKE 11,00",
    "JULMUST SOCKERFR1.4L 16,95 +PANT ENG PET >1L 2,00",
    "LEMON LIME 16,00 +PANT ENG PET <=1L 1,00",
    "POPCORNKÄRNOR 500G + 2st+17,95 35,90",
    "MAGNUM WONDER 12,00",
    "Totalt 8 varor",
    "Totalt 135,73 SEK",
    "Mottaget Kontokort 135,73",
    "MasterCard RROKRKRARARAFAHB",
    "KOP 135.73 SEK",
    "Butik:++0237",
    "Ref: 200262304826 Term: 20026230",
    "TVR: 0000048001 AID: A0000000041010",
    "2024-10-25 15:47:55 TSI: E800",
    "Kontaktlös KA1 7 001 SWE 222575",
    "Moms?s Moms Netto Brutto",
    "12,00 14,54 121,19 135, 73",
    "Poänggrundande belopp: 132,73",
    "x = Ej Bonusgrundande varor: 3,00",
    "Medlemsnummer: 9752299157404517",
    "SPARA KVITTOT",
    "Orgnr :556113-8826",
    "välkommen åter",
    "Du är väl bonuskund?",
    "Om inte - säg till i kassan nästa gång",
    "så löser vi det på 1 minut",
    "Du betjänades av",
    "Självcheckout Kassör",
    "Kassa: 5/132 2024-10-25 15:47"
};
        public static List<string> ExpectedOutcomeOfCombineAndInsertKeyBeforeValue { get; set; } = new()
{
    "Hemköp Gallerian",
    "Tel : 016-131040",
    "Öppettider",
    "Mån-Fre 7-22, Lör-Sön 8-22",
    "MJÖLK 33 1L 17,95",
    "GODIS LV",
    "0,330kg>+69, 50kr/kg 22,93",
    "CARAMEL MILKSHAKE 11,00",
    "JULMUST SOCKERFR1.4L +PANT ENG PET >1L 2,00 16,95",
    "LEMON LIME +PANT ENG PET <=1L 1,00 16,00",
    "POPCORNKÄRNOR 500G + 2st+17,95 35,90",
    "MAGNUM WONDER 12,00",
    "Totalt 8 varor",
    "Totalt 135,73 SEK",
    "Mottaget Kontokort 135,73",
    "MasterCard RROKRKRARARAFAHB",
    "KOP 135.73 SEK",
    "Butik:++0237",
    "Ref: 200262304826 Term: 20026230",
    "TVR: 0000048001 AID: A0000000041010",
    "2024-10-25 15:47:55 TSI: E800",
    "Kontaktlös KA1 7 001 SWE 222575",
    "Moms?s Moms Netto Brutto",
    "12,00 14,54 121,19 135, 73",
    "Poänggrundande belopp: 132,73",
    "x = Ej Bonusgrundande varor: 3,00",
    "Medlemsnummer: 9752299157404517",
    "SPARA KVITTOT",
    "Orgnr :556113-8826",
    "välkommen åter",
    "Du är väl bonuskund?",
    "Om inte - säg till i kassan nästa gång",
    "så löser vi det på 1 minut",
    "Du betjänades av",
    "Självcheckout Kassör",
    "Kassa: 5/132 2024-10-25 15:47"
};
        public static DateTime ExpetedResultOfExtractPurchaseDateParsesDateCorrectl { get; set; } =  new DateTime(2024, 10, 25, 15, 47, 55);
        public static string ExpectedResultOfExtractReceiptStoreDetectsStoreNameCorrectly { get; set; } = "Hemköp Gallerian";
        public static int ExpectedCountOfEctractReceiptItemlines { get; set; } = 12;
        public static int ExpectedCountOfItemlinesAterREmoveNonItemLines {get;set; } = 7;
    }
}
