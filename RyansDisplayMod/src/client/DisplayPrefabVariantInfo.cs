using JimmysUnityUtilities;
using LogicWorld.Interfaces;
using LogicWorld.Rendering.Dynamics;
using LogicWorld.SharedCode.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RyansDisplayMod.Client
{
    public abstract class DisplayVariantInfo : PrefabVariantInfo
    {
        public override abstract string ComponentTextID { get; }

        public override ComponentVariant GenerateVariant(PrefabVariantIdentifier identifier)
        {
            Block[] blocks = new Block[74];
            ComponentInput[] inputs = new ComponentInput[identifier.InputCount];
            for (int i = 0; i < 72; i++)
            {
                int xp = (i % 6);
                int yp = (int)(Math.Floor(i / 6f));
                blocks[i] = new Block
                {
                    RawColor = Color24.Black,
                    Position = new Vector3(xp * (1f / 6f) - (0.5f - (1f / 12f)), 0.25f, yp * (1f / 6f) - (0.5f - (1f / 12f))),
                    Scale = new Vector3((1f / 6f), 0.125f, (1f / 6f))
                };
            }
            blocks[72] = new Block
            {
                RawColor = Color24.Black,
                Position = new Vector3(0, 0f, 0.5f),
                Scale = new Vector3(1f, 0.25f, 2f)
            };
            blocks[73] = new Block
            {
                Position = new Vector3(-0.45f, 0f, -0.45f),
                Rotation = new Vector3(180f, 270f, 0f),
                Scale = new Vector3(2f, 1f, 1f),
                MeshName = "OriginCube_OpenBottom",
                ColliderData = new ColliderData
                {
                    Transform = new ColliderTransform
                    {
                        LocalScale = new Vector3(1f, 0.4f, 1f),
                        LocalPosition = new Vector3(0f, 0.6f, 0f)
                    }
                }
            };
            for (int i = 0; i < inputs.Length - 2; i++)
            {
                var row = i / 2;
                var col = i % 2;
                var length = i / Convert.ToSingle(inputs.Length - 2) * 0.6f + 0.4f;
                inputs[i] = new ComponentInput
                {
                    Position = new Vector3((col) / 3f + 0.0416666667f, -1f, (row) / 3f + 0.0416666667f),
                    Rotation = new Vector3(180f, 0f, 0f),
                    Length = length
                };
            }
            inputs[8] = new ComponentInput
            {
                Position = new Vector3((-1) / 3f + 0.0416666667f, -1f, (0) / 3f + 0.0416666667f),
                Rotation = new Vector3(180f, 0f, 0f),
                Length = 0.6f
            };
            inputs[9] = new ComponentInput
            {
                Position = new Vector3((0) / 3f + 0.0416666667f, -1f, (-1) / 3f + 0.0416666667f),
                Rotation = new Vector3(180f, 0f, 0f),
                Length = 0.6f
            };
            ComponentVariant componentVariant = new ComponentVariant();
            componentVariant.VariantPrefab = new Prefab
            {
                Blocks = blocks,
                Inputs = inputs,
            };
            return componentVariant;
        }

        public override PrefabVariantIdentifier GetDefaultComponentVariant()
        {
            return new PrefabVariantIdentifier(10, 0);
        }
    }
}