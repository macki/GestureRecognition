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

        public string[] GetFirstRow(string dataSetName, string videoName)
        {
            if (dataSetName.Contains("devel"))
            {
                int num = int.Parse(dataSetName.Substring(5, dataSetName.Length - 5));

                foreach (var item in _parsedData)
                {
                    if (item[1] == num.ToString() && item[0] == "devel")
                    {
                        return item;
                    }
                }

            }
            return null;
        }
    }
}
