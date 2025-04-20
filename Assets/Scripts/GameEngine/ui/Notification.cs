using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace GameEngine.ui
{
    public class Notification : MonoBehaviour
    {
        public ProgressBar progressBar;
        public GameObject notificationHolder;

        public GameObject hiddenPosition;
        public GameObject shownPosition;

        private CancellationTokenSource source;

        public async void updateStress(int stress)
        {
            await notificationHolder.transform.DOMove(shownPosition.transform.position, 0.2f).ToUniTask();
            await progressBar.animateProgress(stress / 100f);
            source?.Cancel();

            source = new CancellationTokenSource();
            await UniTask.WaitForSeconds(10f, cancellationToken: source.Token).SuppressCancellationThrow();
            await notificationHolder.transform.DOMove(hiddenPosition.transform.position, 0.2f)
                .WithCancellation(source.Token);
        }
    }
}