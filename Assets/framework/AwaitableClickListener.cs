using Cysharp.Threading.Tasks;
using UnityEngine;

namespace utils
{
    public class AwaitableClickListener
    {
        private UniTaskCompletionSource<Vector3> _completionSource;

        public void notifyClick()
        {
            _completionSource?.TrySetResult(Input.mousePosition);
        }
        
        public async UniTask<Vector3> awaitClick()
        {
            _completionSource?.TrySetCanceled();
            _completionSource = new();
            return await _completionSource.Task;
        } 
    }
}