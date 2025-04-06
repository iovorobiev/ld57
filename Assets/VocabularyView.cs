using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameEngine;
using GameEngine.Comments;
using GameEngine.Comments.CommentsData;
using UnityEngine;

public class VocabularyView : MonoBehaviour
{
    public GameObject content;

    private List<Comment> vocabulary = new();
    private List<CommentVocabItem> instantiated = new();

    public void Awake()
    {
        Game.vocabularyView = this;
    }

    public async UniTask resetVocab(List<Comment> vocabulary)
    {
        foreach (var obj in instantiated)
        {
            Destroy(obj.gameObject);
        }
        instantiated.Clear();
        
        this.vocabulary = vocabulary;
        
        foreach (var comm in this.vocabulary)
        {
            instantiateComment(comm);
        }
        
    }

    private void instantiateComment(Comment comm)
    {
        var item = Resources.Load(Comment.PATH_TO_COMMENT_PREFAB + "VocabItem") as GameObject;
        var commentObject = Instantiate(item, content.transform);
        var commentVocabItem = commentObject.GetComponent<CommentVocabItem>();
        commentVocabItem.setComment(comm);
        instantiated.Add(commentVocabItem);
    }

    public async UniTask removeComment(Comment comment)
    {
        var found = instantiated.Find((item) => item.comment == comment);
        found.Disappear();
        instantiated.Remove(found);
    }

    public async UniTask addComment(Comment comment)
    {
        instantiateComment(comment);   
    }
}
