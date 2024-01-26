﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator
{
    internal static class Static
    {
        public static Dictionary<int, Dictionary<string, Dictionary<string, int>>> VersionLimitTable = new Dictionary<int, Dictionary<string, Dictionary<string, int>>>
    {{1, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 41}, {"A", 25}, {"B", 17}, {"K", 10}}}, {"M", new Dictionary<string, int> {{"N", 34}, {"A", 20}, {"B", 14}, {"K", 8}}}, {"Q", new Dictionary<string, int> {{"N", 27}, {"A", 16}, {"B", 11}, {"K", 7}}}, {"H", new Dictionary<string, int> {{"N", 17}, {"A", 10}, {"B", 7}, {"K", 4}}}}},{2, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 77}, {"A", 47}, {"B", 32}, {"K", 20}}}, {"M", new Dictionary<string, int> {{"N", 63}, {"A", 38}, {"B", 26}, {"K", 16}}}, {"Q", new Dictionary<string, int> {{"N", 48}, {"A", 29}, {"B", 20}, {"K", 12}}}, {"H", new Dictionary<string, int> {{"N", 34}, {"A", 20}, {"B", 14}, {"K", 8}}}}},{3, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 127}, {"A", 77}, {"B", 53}, {"K", 32}}}, {"M", new Dictionary<string, int> {{"N", 101}, {"A", 61}, {"B", 42}, {"K", 26}}}, {"Q", new Dictionary<string, int> {{"N", 77}, {"A", 47}, {"B", 32}, {"K", 20}}}, {"H", new Dictionary<string, int> {{"N", 58}, {"A", 35}, {"B", 24}, {"K", 15}}}}},{4, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 187}, {"A", 114}, {"B", 78}, {"K", 48}}}, {"M", new Dictionary<string, int> {{"N", 149}, {"A", 90}, {"B", 62}, {"K", 38}}}, {"Q", new Dictionary<string, int> {{"N", 111}, {"A", 67}, {"B", 46}, {"K", 28}}}, {"H", new Dictionary<string, int> {{"N", 82}, {"A", 50}, {"B", 34}, {"K", 21}}}}},{5, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 255}, {"A", 154}, {"B", 106}, {"K", 65}}}, {"M", new Dictionary<string, int> {{"N", 202}, {"A", 122}, {"B", 84}, {"K", 52}}}, {"Q", new Dictionary<string, int> {{"N", 144}, {"A", 87}, {"B", 60}, {"K", 37}}}, {"H", new Dictionary<string, int> {{"N", 106}, {"A", 64}, {"B", 44}, {"K", 27}}}}},{6, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 322}, {"A", 195}, {"B", 134}, {"K", 82}}}, {"M", new Dictionary<string, int> {{"N", 255}, {"A", 154}, {"B", 106}, {"K", 65}}}, {"Q", new Dictionary<string, int> {{"N", 178}, {"A", 108}, {"B", 74}, {"K", 45}}}, {"H", new Dictionary<string, int> {{"N", 139}, {"A", 84}, {"B", 58}, {"K", 36}}}}},{7, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 370}, {"A", 224}, {"B", 154}, {"K", 95}}}, {"M", new Dictionary<string, int> {{"N", 293}, {"A", 178}, {"B", 122}, {"K", 75}}}, {"Q", new Dictionary<string, int> {{"N", 207}, {"A", 125}, {"B", 86}, {"K", 53}}}, {"H", new Dictionary<string, int> {{"N", 154}, {"A", 93}, {"B", 64}, {"K", 39}}}}},{8, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 461}, {"A", 279}, {"B", 192}, {"K", 118}}}, {"M", new Dictionary<string, int> {{"N", 365}, {"A", 221}, {"B", 152}, {"K", 93}}}, {"Q", new Dictionary<string, int> {{"N", 259}, {"A", 157}, {"B", 108}, {"K", 66}}}, {"H", new Dictionary<string, int> {{"N", 202}, {"A", 122}, {"B", 84}, {"K", 52}}}}},{9, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 552}, {"A", 335}, {"B", 230}, {"K", 141}}}, {"M", new Dictionary<string, int> {{"N", 432}, {"A", 262}, {"B", 180}, {"K", 111}}}, {"Q", new Dictionary<string, int> {{"N", 312}, {"A", 189}, {"B", 130}, {"K", 80}}}, {"H", new Dictionary<string, int> {{"N", 235}, {"A", 143}, {"B", 98}, {"K", 60}}}}},{10, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 652}, {"A", 395}, {"B", 271}, {"K", 167}}}, {"M", new Dictionary<string, int> {{"N", 513}, {"A", 311}, {"B", 213}, {"K", 131}}}, {"Q", new Dictionary<string, int> {{"N", 364}, {"A", 221}, {"B", 151}, {"K", 93}}}, {"H", new Dictionary<string, int> {{"N", 288}, {"A", 174}, {"B", 119}, {"K", 74}}}}},{11, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 772}, {"A", 468}, {"B", 321}, {"K", 198}}}, {"M", new Dictionary<string, int> {{"N", 604}, {"A", 366}, {"B", 251}, {"K", 155}}}, {"Q", new Dictionary<string, int> {{"N", 427}, {"A", 259}, {"B", 177}, {"K", 109}}}, {"H", new Dictionary<string, int> {{"N", 331}, {"A", 200}, {"B", 137}, {"K", 85}}}}},{12, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 883}, {"A", 535}, {"B", 367}, {"K", 226}}}, {"M", new Dictionary<string, int> {{"N", 691}, {"A", 419}, {"B", 287}, {"K", 177}}}, {"Q", new Dictionary<string, int> {{"N", 489}, {"A", 296}, {"B", 203}, {"K", 125}}}, {"H", new Dictionary<string, int> {{"N", 374}, {"A", 227}, {"B", 155}, {"K", 96}}}}},{13, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1022}, {"A", 619}, {"B", 425}, {"K", 262}}}, {"M", new Dictionary<string, int> {{"N", 796}, {"A", 483}, {"B", 331}, {"K", 204}}}, {"Q", new Dictionary<string, int> {{"N", 580}, {"A", 352}, {"B", 241}, {"K", 149}}}, {"H", new Dictionary<string, int> {{"N", 427}, {"A", 259}, {"B", 177}, {"K", 109}}}}},{14, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1101}, {"A", 667}, {"B", 458}, {"K", 282}}}, {"M", new Dictionary<string, int> {{"N", 871}, {"A", 528}, {"B", 362}, {"K", 223}}}, {"Q", new Dictionary<string, int> {{"N", 621}, {"A", 376}, {"B", 258}, {"K", 159}}}, {"H", new Dictionary<string, int> {{"N", 468}, {"A", 283}, {"B", 194}, {"K", 120}}}}},{15, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1250}, {"A", 758}, {"B", 520}, {"K", 320}}}, {"M", new Dictionary<string, int> {{"N", 991}, {"A", 600}, {"B", 412}, {"K", 254}}}, {"Q", new Dictionary<string, int> {{"N", 703}, {"A", 426}, {"B", 292}, {"K", 180}}}, {"H", new Dictionary<string, int> {{"N", 530}, {"A", 321}, {"B", 220}, {"K", 136}}}}},{16, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1408}, {"A", 854}, {"B", 586}, {"K", 361}}}, {"M", new Dictionary<string, int> {{"N", 1082}, {"A", 656}, {"B", 450}, {"K", 277}}}, {"Q", new Dictionary<string, int> {{"N", 775}, {"A", 470}, {"B", 322}, {"K", 198}}}, {"H", new Dictionary<string, int> {{"N", 602}, {"A", 365}, {"B", 250}, {"K", 154}}}}},{17, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1548}, {"A", 938}, {"B", 644}, {"K", 397}}}, {"M", new Dictionary<string, int> {{"N", 1212}, {"A", 734}, {"B", 504}, {"K", 310}}}, {"Q", new Dictionary<string, int> {{"N", 876}, {"A", 531}, {"B", 364}, {"K", 224}}}, {"H", new Dictionary<string, int> {{"N", 674}, {"A", 408}, {"B", 280}, {"K", 173}}}}},{18, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1725}, {"A", 1046}, {"B", 718}, {"K", 442}}}, {"M", new Dictionary<string, int> {{"N", 1346}, {"A", 816}, {"B", 560}, {"K", 345}}}, {"Q", new Dictionary<string, int> {{"N", 948}, {"A", 574}, {"B", 394}, {"K", 243}}}, {"H", new Dictionary<string, int> {{"N", 746}, {"A", 452}, {"B", 310}, {"K", 191}}}}},{19, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 1903}, {"A", 1153}, {"B", 792}, {"K", 488}}}, {"M", new Dictionary<string, int> {{"N", 1500}, {"A", 909}, {"B", 624}, {"K", 384}}}, {"Q", new Dictionary<string, int> {{"N", 1063}, {"A", 644}, {"B", 442}, {"K", 272}}}, {"H", new Dictionary<string, int> {{"N", 813}, {"A", 493}, {"B", 338}, {"K", 208}}}}},{20, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 2061}, {"A", 1249}, {"B", 858}, {"K", 528}}}, {"M", new Dictionary<string, int> {{"N", 1600}, {"A", 970}, {"B", 666}, {"K", 410}}}, {"Q", new Dictionary<string, int> {{"N", 1159}, {"A", 702}, {"B", 482}, {"K", 297}}}, {"H", new Dictionary<string, int> {{"N", 919}, {"A", 557}, {"B", 382}, {"K", 235}}}}},{21, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 2232}, {"A", 1352}, {"B", 929}, {"K", 572}}}, {"M", new Dictionary<string, int> {{"N", 1708}, {"A", 1035}, {"B", 711}, {"K", 438}}}, {"Q", new Dictionary<string, int> {{"N", 1224}, {"A", 742}, {"B", 509}, {"K", 314}}}, {"H", new Dictionary<string, int> {{"N", 969}, {"A", 587}, {"B", 403}, {"K", 248}}}}},{22, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 2409}, {"A", 1460}, {"B", 1003}, {"K", 618}}}, {"M", new Dictionary<string, int> {{"N", 1872}, {"A", 1134}, {"B", 779}, {"K", 480}}}, {"Q", new Dictionary<string, int> {{"N", 1358}, {"A", 823}, {"B", 565}, {"K", 348}}}, {"H", new Dictionary<string, int> {{"N", 1056}, {"A", 640}, {"B", 439}, {"K", 270}}}}},{23, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 2620}, {"A", 1588}, {"B", 1091}, {"K", 672}}}, {"M", new Dictionary<string, int> {{"N", 2059}, {"A", 1248}, {"B", 857}, {"K", 528}}}, {"Q", new Dictionary<string, int> {{"N", 1468}, {"A", 890}, {"B", 611}, {"K", 376}}}, {"H", new Dictionary<string, int> {{"N", 1108}, {"A", 672}, {"B", 461}, {"K", 284}}}}},{24, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 2812}, {"A", 1704}, {"B", 1171}, {"K", 721}}}, {"M", new Dictionary<string, int> {{"N", 2188}, {"A", 1326}, {"B", 911}, {"K", 561}}}, {"Q", new Dictionary<string, int> {{"N", 1588}, {"A", 963}, {"B", 661}, {"K", 407}}}, {"H", new Dictionary<string, int> {{"N", 1228}, {"A", 744}, {"B", 511}, {"K", 315}}}}},{25, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 3057}, {"A", 1853}, {"B", 1273}, {"K", 784}}}, {"M", new Dictionary<string, int> {{"N", 2395}, {"A", 1451}, {"B", 997}, {"K", 614}}}, {"Q", new Dictionary<string, int> {{"N", 1718}, {"A", 1041}, {"B", 715}, {"K", 440}}}, {"H", new Dictionary<string, int> {{"N", 1286}, {"A", 779}, {"B", 535}, {"K", 330}}}}},{26, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 3283}, {"A", 1990}, {"B", 1367}, {"K", 842}}}, {"M", new Dictionary<string, int> {{"N", 2544}, {"A", 1542}, {"B", 1059}, {"K", 652}}}, {"Q", new Dictionary<string, int> {{"N", 1804}, {"A", 1094}, {"B", 751}, {"K", 462}}}, {"H", new Dictionary<string, int> {{"N", 1425}, {"A", 864}, {"B", 593}, {"K", 365}}}}},{27, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 3517}, {"A", 2132}, {"B", 1465}, {"K", 902}}}, {"M", new Dictionary<string, int> {{"N", 2701}, {"A", 1637}, {"B", 1125}, {"K", 692}}}, {"Q", new Dictionary<string, int> {{"N", 1933}, {"A", 1172}, {"B", 805}, {"K", 496}}}, {"H", new Dictionary<string, int> {{"N", 1501}, {"A", 910}, {"B", 625}, {"K", 385}}}}},{28, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 3669}, {"A", 2223}, {"B", 1528}, {"K", 940}}}, {"M", new Dictionary<string, int> {{"N", 2857}, {"A", 1732}, {"B", 1190}, {"K", 732}}}, {"Q", new Dictionary<string, int> {{"N", 2085}, {"A", 1263}, {"B", 868}, {"K", 534}}}, {"H", new Dictionary<string, int> {{"N", 1581}, {"A", 958}, {"B", 658}, {"K", 405}}}}},{29, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 3909}, {"A", 2369}, {"B", 1628}, {"K", 1002}}}, {"M", new Dictionary<string, int> {{"N", 3035}, {"A", 1839}, {"B", 1264}, {"K", 778}}}, {"Q", new Dictionary<string, int> {{"N", 2181}, {"A", 1322}, {"B", 908}, {"K", 559}}}, {"H", new Dictionary<string, int> {{"N", 1677}, {"A", 1016}, {"B", 698}, {"K", 430}}}}},{30, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 4158}, {"A", 2520}, {"B", 1732}, {"K", 1066}}}, {"M", new Dictionary<string, int> {{"N", 3289}, {"A", 1994}, {"B", 1370}, {"K", 843}}}, {"Q", new Dictionary<string, int> {{"N", 2358}, {"A", 1429}, {"B", 982}, {"K", 604}}}, {"H", new Dictionary<string, int> {{"N", 1782}, {"A", 1080}, {"B", 742}, {"K", 457}}}}},{31, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 4417}, {"A", 2677}, {"B", 1840}, {"K", 1132}}}, {"M", new Dictionary<string, int> {{"N", 3486}, {"A", 2113}, {"B", 1452}, {"K", 894}}}, {"Q", new Dictionary<string, int> {{"N", 2473}, {"A", 1499}, {"B", 1030}, {"K", 634}}}, {"H", new Dictionary<string, int> {{"N", 1897}, {"A", 1150}, {"B", 790}, {"K", 486}}}}},{32, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 4686}, {"A", 2840}, {"B", 1952}, {"K", 1201}}}, {"M", new Dictionary<string, int> {{"N", 3693}, {"A", 2238}, {"B", 1538}, {"K", 947}}}, {"Q", new Dictionary<string, int> {{"N", 2670}, {"A", 1618}, {"B", 1112}, {"K", 684}}}, {"H", new Dictionary<string, int> {{"N", 2022}, {"A", 1226}, {"B", 842}, {"K", 518}}}}},{33, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 4965}, {"A", 3009}, {"B", 2068}, {"K", 1273}}}, {"M", new Dictionary<string, int> {{"N", 3909}, {"A", 2369}, {"B", 1628}, {"K", 1002}}}, {"Q", new Dictionary<string, int> {{"N", 2805}, {"A", 1700}, {"B", 1168}, {"K", 719}}}, {"H", new Dictionary<string, int> {{"N", 2157}, {"A", 1307}, {"B", 898}, {"K", 553}}}}},{34, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 5253}, {"A", 3183}, {"B", 2188}, {"K", 1347}}}, {"M", new Dictionary<string, int> {{"N", 4134}, {"A", 2506}, {"B", 1722}, {"K", 1060}}}, {"Q", new Dictionary<string, int> {{"N", 2949}, {"A", 1787}, {"B", 1228}, {"K", 756}}}, {"H", new Dictionary<string, int> {{"N", 2301}, {"A", 1394}, {"B", 958}, {"K", 590}}}}},{35, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 5529}, {"A", 3351}, {"B", 2303}, {"K", 1417}}}, {"M", new Dictionary<string, int> {{"N", 4343}, {"A", 2632}, {"B", 1809}, {"K", 1113}}}, {"Q", new Dictionary<string, int> {{"N", 3081}, {"A", 1867}, {"B", 1283}, {"K", 790}}}, {"H", new Dictionary<string, int> {{"N", 2361}, {"A", 1431}, {"B", 983}, {"K", 605}}}}},{36, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 5836}, {"A", 3537}, {"B", 2431}, {"K", 1496}}}, {"M", new Dictionary<string, int> {{"N", 4588}, {"A", 2780}, {"B", 1911}, {"K", 1176}}}, {"Q", new Dictionary<string, int> {{"N", 3244}, {"A", 1966}, {"B", 1351}, {"K", 832}}}, {"H", new Dictionary<string, int> {{"N", 2524}, {"A", 1530}, {"B", 1051}, {"K", 647}}}}},{37, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 6153}, {"A", 3729}, {"B", 2563}, {"K", 1577}}}, {"M", new Dictionary<string, int> {{"N", 4775}, {"A", 2894}, {"B", 1989}, {"K", 1224}}}, {"Q", new Dictionary<string, int> {{"N", 3417}, {"A", 2071}, {"B", 1423}, {"K", 876}}}, {"H", new Dictionary<string, int> {{"N", 2625}, {"A", 1591}, {"B", 1093}, {"K", 673}}}}},{38, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 6479}, {"A", 3927}, {"B", 2699}, {"K", 1661}}}, {"M", new Dictionary<string, int> {{"N", 5039}, {"A", 3054}, {"B", 2099}, {"K", 1292}}}, {"Q", new Dictionary<string, int> {{"N", 3599}, {"A", 2181}, {"B", 1499}, {"K", 923}}}, {"H", new Dictionary<string, int> {{"N", 2735}, {"A", 1658}, {"B", 1139}, {"K", 701}}}}},{39, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 6743}, {"A", 4087}, {"B", 2809}, {"K", 1729}}}, {"M", new Dictionary<string, int> {{"N", 5313}, {"A", 3220}, {"B", 2213}, {"K", 1362}}}, {"Q", new Dictionary<string, int> {{"N", 3791}, {"A", 2298}, {"B", 1579}, {"K", 972}}}, {"H", new Dictionary<string, int> {{"N", 2927}, {"A", 1774}, {"B", 1219}, {"K", 750}}}}},{40, new Dictionary<string, Dictionary<string, int>> {{"L", new Dictionary<string, int> {{"N", 7089}, {"A", 4296}, {"B", 2953}, {"K", 1817}}}, {"M", new Dictionary<string, int> {{"N", 5596}, {"A", 3391}, {"B", 2331}, {"K", 1435}}}, {"Q", new Dictionary<string, int> {{"N", 3993}, {"A", 2420}, {"B", 1663}, {"K", 1024}}}, {"H", new Dictionary<string, int> {{"N", 3057}, {"A", 1852}, {"B", 1273}, {"K", 784}}}}}};
    }
}
