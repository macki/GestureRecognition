﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data;
using GestureRecognition.Data.Models;
using GestureRecognition.Data.DataSerialization;
using System.Drawing;

namespace GestureRecognition.SquaresRecognizer.Logic
{
    public class BodyPartSquaresRecognizer_Hands : BodyPartSquaresRecognizer,  IBodyRecognizer
    {
        private List<SelectionSquares> _TrainedItems;
        private List<Rectangle> _Hands;
        private List<Rectangle> _Head;
        private List<Rectangle> _Tors;
        private List<Rectangle> _BottomTors;

        public BodyPartSquaresRecognizer_Hands(List<System.Drawing.Rectangle> bodyToRecognize, List<System.Drawing.Rectangle> selectedPattern, List<SelectionSquares> _trainedItems)
        {
            this._bodyToRecognize = new SelectionSquares() {WholePattern = new List<Rectangle>(bodyToRecognize), ProperPattern = selectedPattern, BodyPart = (int)Enums.BodyPart.Hands};
            this._TrainedItems = _trainedItems;

            _Hands = new List<Rectangle>();
            _Head = new List<Rectangle>();
            _Tors = new List<Rectangle>();
            _BottomTors = new List<Rectangle>();
        }

        public List<Data.Models.SelectionSquares> GetKnownsPattern(string fileName)
        {
            return SerializeToXml<SelectionSquares>.Deserialize(fileName, false);
        }

        public IEnumerable<Rectangle> RecognizeBodyPart()
        {
            _bodyToRecognize.CalculateFullBodyCentroid();

            double avarageHeadWidth = 0;
            double avarageHeadHeight = 0;
            double avarageBodyRatio = 0;
            double avarageBodyElement = 0;
            foreach (var item in _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Hands).ToList())
            {
                avarageHeadWidth += item.PatternWidth;
                avarageBodyRatio += item.BodyRatio;
                avarageHeadHeight += item.PatternHeight;
                avarageBodyElement += item.WholePattern.Count;
            }

            var count = _TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Hands).ToList().Count;

            //1 - Removes Head with Torso
            GetHeadElements();
            GetTorsoElements();
            GetBottomTorso();
            RemoveHeadTorso();

            //2 - hands analysis
            RemoveSquaresWithoutNeighbor();

            return _bodyToRecognize.WholePattern; 
        }

        private void RemoveSquaresWithoutNeighbor()
        {
            var size = _bodyToRecognize.WholePattern[0].Width;
            
            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                bool hasNaighbor = false;

                for (int j = _bodyToRecognize.WholePattern.Count - 1; j >= 0; j--)
                {
                    if ((
                        _bodyToRecognize.WholePattern[i].X + size == _bodyToRecognize.WholePattern[j].X ||
                        _bodyToRecognize.WholePattern[i].X - size == _bodyToRecognize.WholePattern[j].X) && (
                        _bodyToRecognize.WholePattern[i].Y == _bodyToRecognize.WholePattern[j].Y ||
                        _bodyToRecognize.WholePattern[i].Y - size == _bodyToRecognize.WholePattern[j].Y ||
                        _bodyToRecognize.WholePattern[i].Y + size == _bodyToRecognize.WholePattern[j].Y)
                        )
                    {
                        break;
                    }
                    else if (_bodyToRecognize.WholePattern[i].X == _bodyToRecognize.WholePattern[j].X && (
                            _bodyToRecognize.WholePattern[i].Y - size == _bodyToRecognize.WholePattern[j].Y ||
                            _bodyToRecognize.WholePattern[i].Y + size == _bodyToRecognize.WholePattern[j].Y)
                        )
                    {
                        break;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            _bodyToRecognize.WholePattern.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }

        private void RemoveHeadTorso()
        {
            _Tors.AddRange(_Head);
            _Tors.AddRange(_BottomTors);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (_Tors.Contains(_bodyToRecognize.WholePattern[i]))
                {
                    _bodyToRecognize.WholePattern.RemoveAt(i);
                }
            }
        }

        private void GetBottomTorso()
        {
            var minTorso = _Tors.Min(x=>x.X);
            var maxTorso = _Tors.Max(x=>x.X);

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                 if(!_Tors.Contains(_bodyToRecognize.WholePattern[i]))
                 {
                     if (Math.Abs(_bodyToRecognize.WholePattern[i].X - _bodyToRecognize.FullBodyCentroid.X) <= (maxTorso - minTorso) / 2)
                     {
                         if (_bodyToRecognize.WholePattern[i].Y >= _bodyToRecognize.FullBodyCentroid.Y)
                         {
                             _BottomTors.Add(_bodyToRecognize.WholePattern[i]);
                         }
                     }
                 }
            }
        }
        void GetHeadElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Head(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, this._TrainedItems.Where(x => x.BodyPart == (int)Enums.BodyPart.Head).ToList());
            _Head.AddRange(headRecognizer.RecognizeBodyPart());

            for (int i = _bodyToRecognize.WholePattern.Count - 1; i >= 0; i--)
            {
                if (headRecognizer._HeadCentroid.Y + headRecognizer.AvarageHeadHeight / 2 > _bodyToRecognize.WholePattern[i].Y)
                {
                    if (Math.Abs(_bodyToRecognize.WholePattern[i].X - headRecognizer._HeadCentroid.X) <= headRecognizer.AvarageHeadWidth / 2 * 3)
                    {
                        _Head.Add(_bodyToRecognize.WholePattern[i]);
                    }
                }
            }

        }
        void GetTorsoElements()
        {
            var headRecognizer = new BodyPartSquaresRecognizer_Tors(_bodyToRecognize.WholePattern, _bodyToRecognize.ProperPattern, _TrainedItems);
            _Tors.AddRange(headRecognizer.RecognizeBodyPart());
        }
    }
}
