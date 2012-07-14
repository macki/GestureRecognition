using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GestureRecognition.CsvParser
{
    public class CsvParsercs
    {
        private List<string[]> _parsedData;
        public List<string[]> ParsedData
        {
            get { return _parsedData; }
            set { }
        }

        public CsvParsercs()
        {

        }

        public void parseCSV(string path)
        {
            _parsedData = new List<string[]>();

            try
            {
                using (StreamReader readFile = new StreamReader(path))
                {
                    string line;
                    string[] row;

                    while ((line = readFile.ReadLine()) != null)
                    {
                        row = line.Split(',');
                        _parsedData.Add(row);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            }

        }

        public List<string[]> GetDataSetParsedData(string dataSetName, List<string[]> parsedData)
        {
            var videoData = new List<string[]>();

            foreach (var item in parsedData)
            {
                if (item[0] == dataSetName)
                {
                    videoData.Add(item);
                }
            }
            return videoData;
        }
        public List<string[]> GetVidoeParsedData(string videoName,  List<string[]> parsedData)
        {
            var videoData = new List<string[]>();

            foreach (var item in parsedData)
            {
                if (item[1] == videoName)
                {
                    videoData.Add(item);
                }
            }
            return videoData;
        }
        public List<string[]> GetDataSetVidoeParsedData(string dataSetName, string videoName)
        {
            var videoData = GetDataSetParsedData(dataSetName, _parsedData);
            videoData = GetVidoeParsedData(videoName, videoData);
            return videoData;
        }
    }
}
