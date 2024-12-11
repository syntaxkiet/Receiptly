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
        public static string ReceiptSample1Path {get; set;} = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"ReceiptProcessing", "Resources", "ReceiptSample1.jpg");
        public static string TessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
        public static List<string> ExpectedSeparatedCombinedStringsList { get; set; } = new();
    }
}
