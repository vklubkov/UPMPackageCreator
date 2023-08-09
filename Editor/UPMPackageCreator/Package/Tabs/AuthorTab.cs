using System;
using UnityEditor;
using UnityEngine;

namespace UPMPackageCreator {
    [Serializable]
    public class AuthorTab {
        [SerializeField] private GUIContent _nameGuiContent = new GUIContent(Author.Name.Label, Author.Name.Tooltip);
        [SerializeField] private GUIContent _emailGuiContent = new GUIContent(Author.Email.Label, Author.Email.Tooltip);
        [SerializeField] private GUIContent _webPageGuiContent = new GUIContent(Author.Url.Label, Author.Url.Tooltip);

        public void OnGui(AuthorData authorData) {
            DrawVerticalOffset();
            DrawAuthorName(authorData);
            DrawAuthorEmail(authorData);
            DrawAuthorWebsite(authorData);
            DrawVerticalOffset();
        }

        private void DrawVerticalOffset() => GUILayout.Space(15);

        private void DrawAuthorName(AuthorData authorData) =>
            authorData.Name = EditorGUILayout.TextField(_nameGuiContent, authorData.Name);

        private void DrawAuthorEmail(AuthorData authorData) =>
            authorData.Email = EditorGUILayout.TextField(_emailGuiContent, authorData.Email);

        private void DrawAuthorWebsite(AuthorData authorData) =>
            authorData.Website = EditorGUILayout.TextField(_webPageGuiContent, authorData.Website);
    }
}