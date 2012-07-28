using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GestureRecognition.Data.Models;
using GestureRecognition.Data;
using GestureRecognition.Data.DataSerialization;
using System.Windows.Forms;
using GestureRecognition.Data.DataProvider;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    public class SquaresRecognizer
    {
        private SelectionSquares _processingItem;
        private DataProvider _dataProvider;
        private List<SelectionSquares> _trainedItems;

        #region Contructors
        
        public SquaresRecognizer()
        {
            _dataProvider = new DataProvider();
            _trainedItems = new List<SelectionSquares>();

            LoadTrainedData(); 
        }

        #endregion

        #region Methods

        private void LoadTrainedData()
        {
            foreach (var item in _dataProvider.GetSelectionSquares())
            {
                var selectionSquare = SerializeToXml<SelectionSquares>.Deserialize(item.Url, false)[0];
                _trainedItems.Add(selectionSquare);
            }
        }
       
        public void Learn(List<Rectangle> wholePattern, List<Rectangle> pattern, Enums.BodyPart bodyPart )
        {
            _processingItem = new SelectionSquares() { WholePattern = wholePattern, ProperPattern = pattern, BodyPart = (int)bodyPart };
            StartLearning();
        }
        private void StartLearning()
        {
            CalculateBodyParamters();
            Save();
        }
        private void CalculateBodyParamters()
        {
            _processingItem.CalculateBodyRatio();
            _processingItem.CalculateFullBodyCentroid();
            _processingItem.CalculatePatternCentroid();
            _processingItem.CalculatePatternHeight();
            _processingItem.CalculatePatternWidth();
        }
        private void Save()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Gestures (*.xml)|*.xml";
            saveDialog.Title = "Save Gesture As";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = false;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                SerializeToXml<SelectionSquares>.Serialize(new List<SelectionSquares>() { _processingItem }, saveDialog.FileName, false);
                _processingItem.Url = saveDialog.FileName;
                _dataProvider.Save(_processingItem);
            }
        }

        public void Recognize(List<Rectangle> bodyToRecognize, Enums.BodyPart bodyPart)
        {
           switch(bodyPart)
           {
               case Enums.BodyPart.Head:
                   var rec = new BodyPartSquaresRecognizer_Head();
                   break;
           }
        }


        #endregion
    }
}
