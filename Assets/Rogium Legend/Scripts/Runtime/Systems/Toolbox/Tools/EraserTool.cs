// using System;
// using Rogium.Systems.GridSystem;
// using UnityEngine;
//
// namespace Rogium.Systems.Toolbox
// {
//     public class EraserTool<T> : ITool<T> where T : IComparable
//     {
//         public event Action<int, Vector2Int, Sprite> OnGraphicDraw;
//         
//         private readonly T emptyValue;
//         
//         public EraserTool(T emptyValue) => this.emptyValue = emptyValue;
//
//
//         public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer, Action whenEffectFinished)
//         {
//             grid.SetValue(position, emptyValue);
//             OnGraphicDraw?.Invoke(layer, position, true);
//             whenEffectFinished?.Invoke();
//         }
//     }
// }