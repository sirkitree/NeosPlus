﻿using System;
using System.Collections.Generic;
using System.Linq;
using BaseX;
using FrooxEngine.LogiX.Operators;
using FrooxEngine.UIX;
using NEOSPlus;

namespace FrooxEngine.LogiX.Math
{
    [NodeName("BubbleSort")]
    [Category("LogiX/Math")]
    [NodeDefaultType(typeof(BubbleSort<float>))]
    public class BubbleSort<T> : LogixNode where T : IComparable
    {
        public readonly SyncList<Input<T>> ValueInputs;
        public readonly Input<bool> Backward;
        public readonly SyncList<Output<T>> ValueOutputs;

        protected override void OnAttach()
        {
            base.OnAttach();
            for (var i = 0; i < 2; i++)
            {
                ValueOutputs.Add();
                ValueInputs.Add();
            }
        }

        protected override void OnEvaluate()
        {
            var inputs = ValueInputs.Select(t => t.EvaluateRaw()).ToList();
            inputs = BubbleSortAlgorithm(inputs);
            if (Backward.EvaluateRaw()) inputs.Reverse();
            for (var i = 0; i < ValueOutputs.Count; i++) ValueOutputs[i].Value = inputs[i];
        }

        protected override void OnGenerateVisual(Slot root) =>
            GenerateUI(root).GenerateListButtons(Add, Remove);

        protected override Type FindOverload(NodeTypes connectingTypes) =>
            (from input in connectingTypes.inputs
             where input.Key.StartsWith("ValueInputs") &&
                   (typeof(T).GetTypeCastCompatibility(input.Value) == TypeCastCompatibility.Implicit ||
                    ValueInputs.All(i => !i.IsConnected))
             select typeof(BubbleSort<>).MakeGenericType(input.Value)).FirstOrDefault();

        [SyncMethod]
        private void Add(IButton button, ButtonEventData eventData)
        {
            ValueInputs.Add();
            ValueOutputs.Add();
            RefreshLogixBox();
        }

        [SyncMethod]
        private void Remove(IButton button, ButtonEventData eventData)
        {
            if (ValueInputs.Count <= 2) return;
            ValueInputs.RemoveAt(ValueInputs.Count - 1);
            ValueOutputs.RemoveAt(ValueOutputs.Count - 1);
            RefreshLogixBox();
        }

        private List<T> BubbleSortAlgorithm(List<T> list)
        {
            int n = list.Count;
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (list[i - 1].CompareTo(list[i]) > 0)
                    {
                        T temp = list[i - 1];
                        list[i - 1] = list[i];
                        list[i] = temp;
                        swapped = true;
                    }
                }
                n--;
            } while (swapped);

            return list;
        }
    }
}