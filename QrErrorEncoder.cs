using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using QRGenerator.encoders;
using STH1123.ReedSolomon;

namespace QRGenerator
{
    public class QrErrorEncoder
    {
        //dico = {}
        //for line in data.splitlines():
        //    version = line.split("-")[0]
        //    if not version in dico:
        //        dico[version] = {}
        //    level = line.split("-")[1].split("\t")[0]
        //    if not level in dico[version]:
        //        dico[version][level] = {}
        //    dico[version][level] = [int(line.split("\t")[2] or 0), int (line.split("\t")[3] or 0), int (line.split("\t")[4] or 0), int (line.split("\t")[5] or 0), int (line.split("\t")[6] or 0)]

        //def convert_to_csharp(input_dict):
        //    csharp_code = "{"
        //    for key, value in input_dict.items():
        //        inner_dict = ", ".join([f'{{"{inner_key}", new int[5] {{{", ".join(map(str, inner_value))}}}}}' for inner_key, inner_value in value.items()])
        //        csharp_code += f'{{{key}, new Dictionary<string, int[]> {{{inner_dict}}}}}, '
        //    csharp_code = csharp_code[:-2]  # Remove the trailing comma and space
        //    csharp_code += "}"
        //    return csharp_code

        // [Version][CorrectionLevel] = [ECWordPerBlock, BlockGroup1, WordCount1, BlockGroup2, WordCount2]
        private static Dictionary<int, Dictionary<string, int[]>> ErrorCorrectionCodewordsCount = Static.ErrorCorrectionCodewordsCount;

        //dico = {}
        //for line in data.splitlines():
        //    dico[int(line.split("\t")[3] or 0)] = int (line.split("\t")[4] or 0)

        //def convert_to_csharp(input_dict):
        //    csharp_code = "{"
        //    for key, value in input_dict.items():
        //        csharp_code += f'{{{key}, {value}}}, '
        //    csharp_code = csharp_code[:-2]
        //    csharp_code += "}"
        //    return csharp_code
        private ErrorCorrectionLevels ErrorCorrectionLevel { get; set; }
        private int Version { get; set; }
        private int ECWordPerBlock { get; set; }
        private List<List<List<int>>> ErrorEncodedGroups { get; set; }
        public List<int> EncodedData { get; set; }
        private ReedSolomonEncoder Rse = new ReedSolomonEncoder(GenericGF.QR_CODE_FIELD_256);

        /// <summary>
        /// A class to encode the data for a QR code with Reed Solomon 
        /// </summary>
        /// <param name="errorCorrectionLevel"></param>
        /// <param name="version"></param>
        /// <param name="data"></param>
        public QrErrorEncoder(ErrorCorrectionLevels errorCorrectionLevel, int version, string data)
        {
            ErrorCorrectionLevel = errorCorrectionLevel;
            Version = version;
            ECWordPerBlock = ErrorCorrectionCodewordsCount[Version][ErrorCorrectionLevel.ToString()][0];
            // Console.WriteLine("data to encode: " + data);
            List<List<List<string>>> Blocks = BreakUpDataIntoBlocks(data);
            ErrorEncodedGroups = ConvertBlocksToInt(Blocks);

            for (int i = 0; i < ErrorEncodedGroups.Count; i++)
            {
                for (int j = 0; j < ErrorEncodedGroups[i].Count; j++)
                {
                    int[] dataBlock = ErrorEncodedGroups[i][j].ToArray();
                    Rse.Encode(dataBlock, ECWordPerBlock);
                    ErrorEncodedGroups[i][j] = dataBlock.ToList();
                }
            }

            EncodedData = new List<int>();
            MergeBlocks(ErrorEncodedGroups);
        }

        /// <summary>
        /// Break up the data into blocks to be encoded with Reed Solomon
        /// </summary>
        /// <param name="Data"></param>
        /// <returns> A list of group 
        /// 1 group = 1 list of blocks
        /// 1 block = 1 list of data codewords
        /// 1 data codeword = 1 string of 8 bits</returns>
        private List<List<List<string>>> BreakUpDataIntoBlocks(string Data)
        {
            if (Data.Length % 8 != 0)
            {
                throw new Exception("Data length must be a multiple of 8");
            }
            //    split string into codewords :
            // "0100001101010101010001101000011001010111"
            // => [01000011, 01010101, 01000110, 10000110, 01010111]

            // split string into 8 bits :
            string[] stringCodewords = new string[Data.Length / 8];
            for (int i = 0; i < Data.Length; i += 8)
            {
                stringCodewords[i / 8] = Data.Substring(i, 8);
            }
            // Console.WriteLine(Data.Length / 8);
            // Console.WriteLine("String codewords: " + string.Join(", ", stringCodewords));
            // Console.WriteLine("String codewords length: " + stringCodewords.Length);


            // Group Number	    Block Number	Data Codewords in the Group
            int blockGroup1 = ErrorCorrectionCodewordsCount[Version][ErrorCorrectionLevel.ToString()][1];
            int wordCount1 = ErrorCorrectionCodewordsCount[Version][ErrorCorrectionLevel.ToString()][2];
            int blockGroup2 = ErrorCorrectionCodewordsCount[Version][ErrorCorrectionLevel.ToString()][3];
            int wordCount2 = ErrorCorrectionCodewordsCount[Version][ErrorCorrectionLevel.ToString()][4];

            if (Data.Length / 8 != blockGroup1 * wordCount1 + blockGroup2 * wordCount2)
            {
                Console.WriteLine("Version is " + Version + " and ErrorCorrectionLevel is " + ErrorCorrectionLevel);
                Console.WriteLine("data lenght : " + Data.Length / 8 + " Expected : " + (blockGroup1 * wordCount1 + blockGroup2 * wordCount2));
                throw new Exception("Size mismatch");
            }

            // Initialize the blocks arrays
            var blocks = new List<List<List<string>>>
            {
                new List<List<string>>() // Group 1
            };
            for (int i = 0; i < blockGroup1; i++)
            {
                blocks[0].Add(new List<string>()); // Block x
            }

            if (blockGroup2 > 0)
            {
                blocks.Add(new List<List<string>>()); // Group 2
                for (int i = 0; i < blockGroup2; i++)
                {
                    blocks[1].Add(new List<string>()); // Block x
                }
            }


            // Cursor is used to keep track of the current position in the data array
            int cursor = 0;

            // for each group, 2 groups
            int group = 0;
            //  For each block in the group
            for (int block = 0; block < blockGroup1; block++)
            {
                // For each word in the block
                for (int word = 0; word < wordCount1; word++)
                {
                    var a = stringCodewords[cursor];
                    blocks[group][block].Add(a);
                    cursor++;
                }
                // Add the error correction words
                for (int ECWordPerBlockIndex = 0; ECWordPerBlockIndex < ECWordPerBlock; ECWordPerBlockIndex++)
                {
                    blocks[group][block].Add("00000000");
                }
            }

            group = 1;
            // May be 0
            for (int blockGroup2Index = 0; blockGroup2Index < blockGroup2; blockGroup2Index++)
            {
                for (int wordCount2Index = 0; wordCount2Index < wordCount2; wordCount2Index++)
                {
                    blocks[group][blockGroup2Index].Add(stringCodewords[cursor]);
                    cursor++;
                }
                for (int ECWordPerBlockIndex = 0; ECWordPerBlockIndex < ECWordPerBlock; ECWordPerBlockIndex++)
                {
                    blocks[group][blockGroup2Index].Add("00000000");
                }
            }

            return blocks;
        }

        /// <summary>
        /// Convert the blocks from string to bytes
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public static List<List<List<int>>> ConvertBlocksToInt(List<List<List<string>>> blocks)
        {
            var blocksAsInt = new List<List<List<int>>>();
            for (int i = 0; i < blocks.Count; i++)
            {
                blocksAsInt.Add(new List<List<int>>());
                for (int j = 0; j < blocks[i].Count; j++)
                {
                    blocksAsInt[i].Add(new List<int>());
                    for (int k = 0; k < blocks[i][j].Count; k++)
                    {
                        // blocksAsBytes[i][j].Add(Convert.ToByte(blocks[i][j][k], 2));
                        blocksAsInt[i][j].Add(Convert.ToInt32(blocks[i][j][k], 2));
                    }
                }
            }
            return blocksAsInt;
        }

        public void DisplayErrorEncodedGroups()
        {
            for (int i = 0; i < ErrorEncodedGroups.Count; i++)
            {
                Console.WriteLine("Group " + i);
                for (int j = 0; j < ErrorEncodedGroups[i].Count; j++)
                {
                    Console.WriteLine("\tBlock " + j);
                    for (int k = 0; k < ErrorEncodedGroups[i][j].Count; k++)
                    {
                        Console.WriteLine("\t\t" + ErrorEncodedGroups[i][j][k]);
                    }
                }
            }
        }

        public void MergeBlocks(List<List<List<int>>> groups)
        {
            // if there is only 1 block, just merge
            if (groups[0].Count == 1)
            {
                for (int i = 0; i < groups[0].Count; i++)
                {
                    for (int j = 0; j < groups.Count; j++)
                    {
                        EncodedData.AddRange(groups[j][i]);
                    }
                }
            }
            // if there are multiple blocks, merge them by interleaving
            else
            {
                // flatten the groups
                var blocks = new List<List<int>>();
                for (int i = 0; i < groups.Count; i++)
                {
                    blocks.AddRange(groups[i]);
                }

                // merge by interleaving
                for (int i = 0; i < blocks[0].Count; i++) // for each codeword
                {
                    for (int j = 0; j < blocks.Count; j++) // for each block
                    {
                        EncodedData.Add(blocks[j][i]);
                    }
                }
            }

        }
    }
}
