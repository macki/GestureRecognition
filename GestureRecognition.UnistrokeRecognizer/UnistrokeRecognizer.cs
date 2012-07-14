using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestureRecognition.Data.Models;
using GestureRecognition.UnistrokeRecognizer.Logic;
using GestureRecognition.UnistrokeRecognizer.Algorithms;

namespace GestureRecognition.UnistrokeRecognizer
{
    public class UnistrokeRecognizer
    {
        public Gestures Recognize(List<Points> pointsToRecognize, List<Gestures> knownGestures, Enums.UnistrokeRecognizeMode mode)
        {
            switch (mode)
            {
                case Enums.UnistrokeRecognizeMode.basic:
                    {
                        var recognizerKnowGesture = new  BasicUnistrokeRecognizer();
                        var transformGesturem = new List<Gestures>();

                        foreach (var item in knownGestures)
                        {
                            transformGesturem.Add(new Gestures() { Points = recognizerKnowGesture.TransformInputGestures(item.Points) });
                        }


                        var recognizer = new BasicUnistrokeRecognizer(pointsToRecognize, transformGesturem);
                        return recognizer.Result();
                    }break;
                case Enums.UnistrokeRecognizeMode.complex:
                    {
                    
                    }break;
            }
            return null;
        }
    }
}
