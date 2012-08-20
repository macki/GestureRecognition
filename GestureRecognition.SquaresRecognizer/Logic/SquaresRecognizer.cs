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
            _processingItem.CalculateBodyParameters();
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

        public IEnumerable<Rectangle> Recognize(List<Rectangle> wholeBodyToRecognize, List<Rectangle> selectedPattern, Enums.BodyPart bodyPart)
        {
           switch(bodyPart)
           {
               case Enums.BodyPart.Head:
                   var headRecognizer = new BodyPartSquaresRecognizer_Head(wholeBodyToRecognize, selectedPattern, GetSpecifiedBodyPart(bodyPart).ToList());
                   return headRecognizer.RecognizeBodyPart();
               case Enums.BodyPart.Torso:
                   var torsRecognizer = new BodyPartSquaresRecognizer_Tors(wholeBodyToRecognize, selectedPattern, _trainedItems);
                   return torsRecognizer.RecognizeBodyPart();
               case Enums.BodyPart.Hands:
                   var handsRecognizer = new BodyPartSquaresRecognizer_Hands(wholeBodyToRecognize, selectedPattern, _trainedItems);
                   return handsRecognizer.RecognizeBodyPart();
               case Enums.BodyPart.Legs:
                   var legs = new BodyPartSquaresRecognizer_Legs(wholeBodyToRecognize, selectedPattern, _trainedItems);
                   return legs.RecognizeBodyPart();
               case Enums.BodyPart.LeftHand:
                   var LeftHand= new BodyPartSquaresRecognizer_LeftHand(wholeBodyToRecognize, selectedPattern, _trainedItems);
                   return LeftHand.RecognizeBodyPart();
               case Enums.BodyPart.RightHand:
                   var rightHand = new BodyPartSquaresRecognizer_RightHand(wholeBodyToRecognize, selectedPattern, _trainedItems);
                   return rightHand.RecognizeBodyPart();

           }

           return null;
        }

        private IEnumerable<SelectionSquares> GetSpecifiedBodyPart(Enums.BodyPart bodyPart)
        {
            return _trainedItems.Where(x => x.BodyPart == (int)bodyPart);
        }


        #endregion
    }
}
