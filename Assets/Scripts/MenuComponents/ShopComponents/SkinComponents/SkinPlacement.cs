using UnityEngine;

namespace MenuComponents.ShopComponents.SkinComponents
{
    public class SkinPlacement : MonoBehaviour
    {
        private const string RenderLayer = "SkinRender";
        
        [SerializeField] private Rotator _rotator;
        
        private SkinModel _currentModel;
        
        public void InstantiateModel(SkinModel model)
        {
            if(_currentModel != null)
            {
                Destroy(_currentModel.gameObject);
            }

            _rotator.ResetRotation();
            
            _currentModel = Instantiate(model, transform);
            
            Transform[] childrens = _currentModel.GetComponentsInChildren<Transform>();
            
            foreach(Transform item in childrens)
            {
                item.gameObject.layer = LayerMask.NameToLayer(RenderLayer);
            }
        }
        
        public void DestroyModel()
        {
            if(_currentModel != null)
            {
                Destroy(_currentModel.gameObject);
            }
        }
    }
}