using LogicWorld.Rendering.Components;
using JimmysUnityUtilities;
using LogicWorld.ClientCode;
using LogicWorld.ClientCode.Resizing;
using LogicWorld.SharedCode.Components;
using UnityEngine;
using System;
using LogicAPI.Data;

namespace RyansDisplayMod.Client
{
    class MonochromeDisplay : ComponentClientCode<MonochromeDisplay.IData>, IColorableClientCode, IResizableX
    {
        public Color24 Color { get => Data.color; set => Data.color = value; }

        public string ColorsFileKey => "SevenSegments";

        public int SizeX { get => Data.size; set => Data.size = value; }

        public int MinX => 1;

        public int MaxX => 32;

        public float GridIntervalX => 1.0f;
        public float MinColorValue => 0.0f;
        private int prevSize;
        private static int[][] characters = new int[][]
        {
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, },
            new int[] {56, 56, 56, 56, 56, 56, 0, 0, 0, 0, 0, 0, },
            new int[] {63, 63, 63, 63, 63, 63, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, },
            new int[] {7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, },
            new int[] {56, 56, 56, 56, 56, 56, 7, 7, 7, 7, 7, 7, },
            new int[] {63, 63, 63, 63, 63, 63, 7, 7, 7, 7, 7, 7, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 4, 4, 4, 4, 4, 4, 4, 0, 4, 0, 0, },
            new int[] {10, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 10, 10, 31, 10, 10, 31, 10, 10, 0, 0, },
            new int[] {4, 14, 21, 5, 5, 14, 20, 20, 21, 14, 4, 0, },
            new int[] {19, 19, 8, 8, 4, 4, 2, 2, 25, 25, 0, 0, },
            new int[] {0, 14, 17, 17, 9, 6, 21, 9, 9, 22, 0, 0, },
            new int[] {4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {12, 2, 2, 2, 2, 2, 2, 2, 2, 2, 12, 0, },
            new int[] {6, 8, 8, 8, 8, 8, 8, 8, 8, 8, 6, 0, },
            new int[] {21, 14, 31, 14, 21, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 4, 4, 31, 4, 4, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 4, 2, },
            new int[] {0, 0, 0, 0, 0, 31, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 0, 0, },
            new int[] {16, 16, 8, 8, 4, 4, 2, 2, 1, 1, 0, 0, },
            new int[] {0, 31, 17, 17, 17, 17, 17, 17, 17, 31, 0, 0, },
            new int[] {0, 4, 6, 5, 4, 4, 4, 4, 4, 31, 0, 0, },
            new int[] {0, 31, 16, 16, 16, 31, 1, 1, 1, 31, 0, 0, },
            new int[] {0, 31, 16, 16, 16, 31, 16, 16, 16, 31, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 31, 16, 16, 16, 16, 0, 0, },
            new int[] {0, 31, 1, 1, 1, 31, 16, 16, 16, 31, 0, 0, },
            new int[] {0, 31, 1, 1, 1, 31, 17, 17, 17, 31, 0, 0, },
            new int[] {0, 31, 16, 16, 16, 16, 16, 16, 16, 16, 0, 0, },
            new int[] {0, 31, 17, 17, 17, 31, 17, 17, 17, 31, 0, 0, },
            new int[] {0, 31, 17, 17, 17, 31, 16, 16, 16, 31, 0, 0, },
            new int[] {0, 0, 6, 6, 0, 0, 0, 6, 6, 0, 0, 0, },
            new int[] {0, 0, 6, 6, 0, 0, 0, 6, 6, 4, 2, 0, },
            new int[] {0, 0, 0, 8, 4, 2, 4, 8, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 31, 0, 31, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 2, 4, 8, 4, 2, 0, 0, 0, 0, },
            new int[] {0, 14, 17, 17, 16, 8, 4, 4, 0, 4, 0, 0, },
            new int[] {0, 14, 17, 25, 21, 21, 21, 25, 1, 30, 0, 0, },
            new int[] {0, 14, 17, 17, 17, 31, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 15, 17, 17, 17, 15, 17, 17, 17, 15, 0, 0, },
            new int[] {0, 30, 1, 1, 1, 1, 1, 1, 1, 30, 0, 0, },
            new int[] {0, 15, 17, 17, 17, 17, 17, 17, 17, 15, 0, 0, },
            new int[] {0, 31, 1, 1, 1, 31, 1, 1, 1, 31, 0, 0, },
            new int[] {0, 31, 1, 1, 1, 31, 1, 1, 1, 1, 0, 0, },
            new int[] {0, 30, 1, 1, 1, 29, 17, 17, 17, 14, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 31, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 31, 4, 4, 4, 4, 4, 4, 4, 31, 0, 0, },
            new int[] {0, 31, 4, 4, 4, 4, 4, 4, 5, 7, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 15, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 1, 1, 1, 1, 1, 1, 1, 1, 31, 0, 0, },
            new int[] {0, 17, 27, 21, 21, 17, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 17, 19, 19, 21, 21, 21, 25, 25, 17, 0, 0, },
            new int[] {0, 14, 17, 17, 17, 17, 17, 17, 17, 14, 0, 0, },
            new int[] {0, 15, 17, 17, 17, 15, 1, 1, 1, 1, 0, 0, },
            new int[] {0, 14, 17, 17, 17, 17, 17, 21, 9, 22, 0, 0, },
            new int[] {0, 15, 17, 17, 17, 15, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 14, 17, 1, 1, 14, 16, 16, 17, 14, 0, 0, },
            new int[] {0, 31, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 17, 17, 17, 17, 14, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 17, 17, 17, 10, 4, 0, 0, },
            new int[] {0, 17, 17, 17, 17, 17, 21, 21, 27, 17, 0, 0, },
            new int[] {0, 17, 17, 17, 10, 4, 10, 17, 17, 17, 0, 0, },
            new int[] {0, 17, 17, 17, 10, 4, 4, 4, 4, 4, 0, 0, },
            new int[] {0, 31, 16, 16, 8, 4, 2, 1, 1, 31, 0, 0, },
            new int[] {14, 2, 2, 2, 2, 2, 2, 2, 2, 2, 14, 0, },
            new int[] {1, 1, 2, 2, 4, 4, 8, 8, 16, 16, 0, 0, },
            new int[] {14, 8, 8, 8, 8, 8, 8, 8, 8, 8, 14, 0, },
            new int[] {0, 4, 10, 17, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 63, },
            new int[] {3, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 14, 16, 30, 17, 30, 0, 0, },
            new int[] {0, 1, 1, 1, 1, 15, 17, 17, 17, 15, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 30, 1, 1, 1, 30, 0, 0, },
            new int[] {0, 16, 16, 16, 16, 30, 17, 17, 17, 30, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 14, 17, 31, 1, 30, 0, 0, },
            new int[] {0, 28, 2, 2, 2, 31, 2, 2, 2, 2, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 14, 17, 17, 17, 30, 16, 15, },
            new int[] {0, 1, 1, 1, 1, 15, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 0, 4, 0, 0, 4, 4, 4, 4, 4, 0, 0, },
            new int[] {0, 0, 4, 0, 0, 4, 4, 4, 4, 4, 4, 3, },
            new int[] {0, 1, 1, 1, 1, 17, 9, 7, 9, 17, 0, 0, },
            new int[] {0, 7, 4, 4, 4, 4, 4, 4, 4, 31, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 10, 21, 21, 21, 21, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 14, 17, 17, 17, 17, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 14, 17, 17, 17, 14, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 15, 17, 17, 17, 15, 1, 1, },
            new int[] {0, 0, 0, 0, 0, 30, 17, 17, 17, 30, 16, 16, },
            new int[] {0, 0, 0, 0, 0, 13, 19, 1, 1, 1, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 30, 1, 14, 16, 15, 0, 0, },
            new int[] {0, 4, 4, 4, 4, 31, 4, 4, 4, 24, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 17, 17, 17, 17, 14, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 17, 17, 17, 10, 4, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 21, 21, 21, 21, 10, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 17, 10, 4, 10, 17, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 17, 17, 17, 17, 30, 16, 15, },
            new int[] {0, 0, 0, 0, 0, 31, 8, 4, 2, 31, 0, 0, },
            new int[] {12, 4, 4, 4, 4, 2, 4, 4, 4, 4, 12, 0, },
            new int[] {4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 0, },
            new int[] {6, 4, 4, 4, 4, 8, 4, 4, 4, 4, 6, 0, },
            new int[] {0, 0, 0, 0, 2, 21, 8, 0, 0, 0, 0, 0, },
            new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, },
        };

        protected override void DataUpdate()
        {
            if (SizeX != prevSize)
            {
                float newScale = SizeX;
                float offset = (SizeX - 1) * 0.5f;

                for (int i = 0; i < 72; i++)
                {
                    int xp = (i % 6);
                    int yp = (int)(Math.Floor(i / 6f));
                    SetBlockPosition(i, new Vector3(xp * (newScale / 6f) - (0.5f - (newScale / 12f)), 0.25f, yp * (newScale / 6f) - (0.5f - (newScale / 12f))));
                    SetBlockScale(i, new Vector3((newScale / 6f), 0.125f, (newScale / 6f)));
                }
                SetBlockScale(72, new Vector3(newScale, 0.25f, 2 * newScale));
                SetBlockPosition(72, new Vector3(offset, 0, offset * 2 + 0.5f));

                for (int i = 0; i < 8; i++)
                {
                    var row = i / ((SizeX * 3) - 1);
                    var col = i % ((SizeX * 3) - 1);
                    byte ii = Convert.ToByte(i);
                    SetInputPosition(ii, new Vector3((col) / 3f + 0.0416666667f, -1f, (row) / 3f + 0.0416666667f));
                }
                SetBlockScale(73, new Vector3(2f, 1f, newScale));
                prevSize = SizeX;
            }
            QueueFrameUpdate();
        }
        protected override void FrameUpdate()
        {
            if (GetInputState(8) && GetInputState(9))
            {
                GpuColor col = GpuColorConversionExtensions.ToGpuColor(Data.color);
                GpuColor black = GpuColorConversionExtensions.ToGpuColor(Color24.Black);
                int index = GetInputState(0) ? 1 : 0;
                index |= GetInputState(1) ? 2 : 0;
                index |= GetInputState(2) ? 4 : 0;
                index |= GetInputState(3) ? 8 : 0;
                index |= GetInputState(4) ? 16 : 0;
                index |= GetInputState(5) ? 32 : 0;
                index |= GetInputState(6) ? 64 : 0;
                bool invert = GetInputState(7) ? true : false;
                for (int i = 0; i < 12; i++)
                {
                    int num = characters[index][11 - i];
                    for (int j = 0; j < 6; j++)
			    	{
                        if (invert)
                        {
                            SetBlockColor((num % 2 == 1) ? black : col, i * 6 + j);
                        }
                        else
                        {
                            SetBlockColor((num % 2 == 1) ? col : black, i * 6 + j);
                        }
                        num = (int)Math.Floor(num / 2f);
			    	}
                }
            }
        }
        public override PlacingRules GenerateDynamicPlacingRules()
        {
            return PlacingRules.FlippablePanelOfSize(SizeX, SizeX * 2);
        }

        protected override void SetDataDefaultValues()
        {
            Data.color = Color24.Amber;
            Data.size = 1;
        }

        public interface IData
        {
            Color24 color { get; set; }
            int size { get; set; }
        }
    }
}