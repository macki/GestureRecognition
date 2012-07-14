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
        public Gestures Recognize(List<Points> pointsToRecognize, List<Gestures> knownGestures, Enums.RecognizeMode mode)
        {
            switch (mode)
            {
                case Enums.RecognizeMode.Unistroke_DollarOne:
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
                case Enums.RecognizeMode.Unistroke_Protractor:
                    {
                        var recognizer = new PotractorRecognizer(pointsToRecognize, knownGestures);
                        return recognizer.Result();
                    }break;
            }
            return null;
        }
    }
}
