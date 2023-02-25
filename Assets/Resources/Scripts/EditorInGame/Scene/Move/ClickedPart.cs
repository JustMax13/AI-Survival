using System.Threading.Tasks;
using UnityEngine;

namespace Editor.Moves
{
    public class ClickedPart : MonoBehaviour
    {
        private bool _mouseButtonUp;

        private void Start()
        {
            _mouseButtonUp = false;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);

                
                // нужно найти как-то спрайт, который отображается высше другого
                // поход такой параметр нужно самим создавать.
                // Нужно создать отдельный класс, который будет хранить такой счетчик, в какой последовательности
                // были заспавнены детали. Сделать его так, чтобы в дальшейшем можно будет сделать правильную сортировку.
                // возможно, для этого можно использовать то, что в PlayerBot, в дочерние обьекты все добавляется по очереди
                // - сверху вниз

                //foreach (var item in colliders)
                //{
                //    Debug.Log(item);
                //    Debug.Log(item.GetComponent<SpriteRenderer>().);
                //}
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    _mouseButtonDown = true; // тут начать счетчик
            //    Test();
            //}
            //else if (Input.GetMouseButtonUp(0))
            //{
            //    _mouseButtonDown = false;
            //}
            ////    _mouseButtonUp = true; // тут закончить
        }
        private async void Test()// так оно работает
        {
            await Task.Run(() =>
            {
                while (!_mouseButtonUp)
                    Debug.Log("Inside while");

                Debug.Log("OutSide while");
            }
            );
            Debug.Log("OutSide await Task");
        }
    }
}