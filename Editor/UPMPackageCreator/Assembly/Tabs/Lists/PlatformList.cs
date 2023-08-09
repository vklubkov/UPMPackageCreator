using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace UPMPackageCreator {
    [Serializable]
    public class PlatformList {
        [SerializeField] private GUIContent _titleGuiContent =
            new GUIContent(Platforms.Title.Label, Platforms.Title.Tooltip);

        [SerializeField] private bool _anyPlatform;

        private Texture _backgroundTexture;
        private GUIStyle _backgroundStyle;

        private GUILayoutOption _selectButtonGUILayoutOption;
        private GUILayoutOption _deselectButtonGUILayoutOption;

        public void OnEnable(List<string> includePlatforms) {
            _anyPlatform = includePlatforms.Count == 0;
        }

        public void OnGui(List<string> includePlatforms, List<string> excludePlatforms) {
            EditorGUILayout.LabelField(_titleGuiContent, EditorStyles.boldLabel);

            if (_backgroundTexture == null || _backgroundStyle == null) {
                CleanupBackgroundTexture();
                CreateBackgroundStyle();
            }

            if (_selectButtonGUILayoutOption == null)
                CreateSelectButtonLayout();

            if (_deselectButtonGUILayoutOption == null)
                CreateDeselectButtonLayout();

            var oldAnyPlatform = _anyPlatform;
            using (new VerticalLayout(_backgroundStyle)) {
                DrawAnyPlatform();
                DrawPlatformsTitle();
                SwitchPlatformLists(oldAnyPlatform, newAnyPlatform:_anyPlatform, includePlatforms, excludePlatforms);
                DrawPlatformsToggle(includePlatforms, excludePlatforms);
                DrawSmallVerticalSpace();
                DrawSelectionButtons(includePlatforms, excludePlatforms);
            }
        }

        private void CreateBackgroundStyle() {
            var style = new GUIStyle(EditorStyles.label);
            var texture = new Texture2D(1, 1);
            var fillColor = new Color(0f, 0f, 0f, 0.1f);
            texture.SetPixels(new[] { fillColor }, 0 );
            texture.Apply();
            style.normal.background = texture;
            _backgroundTexture = texture;
            _backgroundStyle = style;
        }

        private void CreateSelectButtonLayout() {
            var selectButtonGUIContent = new GUIContent(Platforms.Button.Select.Label);
            _selectButtonGUILayoutOption =
                GUILayout.Width(
                    GUI.skin.label.CalcSize(selectButtonGUIContent).x + Platforms.Button.Width.Offset);
        }

        private void CreateDeselectButtonLayout() {
            var deselectButtonGUIContent = new GUIContent(Platforms.Button.Deselect.Label);
            _deselectButtonGUILayoutOption =
                GUILayout.Width(
                    GUI.skin.label.CalcSize(deselectButtonGUIContent).x + Platforms.Button.Width.Offset);
        }

        private void DrawAnyPlatform() {
            _anyPlatform = EditorGUILayout.Toggle(
                new GUIContent(Platforms.Any.Label, Platforms.Any.Tooltip), _anyPlatform);
        }

        private void DrawPlatformsTitle() {
            var label = _anyPlatform
                ? Platforms.Exclude.Label
                : Platforms.Include.Label;

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        }

        private void SwitchPlatformLists(
            bool oldAnyPlatform, bool newAnyPlatform, List<string> includePlatforms, List<string> excludePlatforms) {
            if (newAnyPlatform == oldAnyPlatform)
                return;

            var oldPlatforms = oldAnyPlatform ? excludePlatforms : includePlatforms;
            var newPlatforms = newAnyPlatform ? excludePlatforms : includePlatforms;
            foreach (var platform in Platforms.All.Keys) {
                if (!oldPlatforms.Contains(platform))
                    newPlatforms.Add(platform);
            }

            oldPlatforms.Clear();
        }

        private void DrawPlatformsToggle(List<string> includePlatforms, List<string> excludePlatforms) {
            var platforms = Platforms.All.Keys;
            var platformNames = Platforms.All.Names;
            for (var i = 0; i < platforms.Count; i++) {
                var platform = platforms[i];
                var platformName = platformNames[i];
                var platformToggle = _anyPlatform
                    ? excludePlatforms.Contains(platform)
                    : includePlatforms.Contains(platform);

                var result = EditorGUILayout.Toggle(platformName, platformToggle);
                if (result == platformToggle)
                    continue;

                var currentPlatforms = _anyPlatform ? excludePlatforms : includePlatforms;
                if (result)
                    currentPlatforms.Add(platform);
                else
                    currentPlatforms.Remove(platform);
            }
        }

        private void DrawSmallVerticalSpace() => GUILayout.Space(5);

        private void DrawSelectionButtons(List<string> includePlatforms, List<string> excludePlatforms) {
            using (new HorizontalLayout()) {
                if (GUILayout.Button(Platforms.Button.Select.Label, _selectButtonGUILayoutOption)) {
                    var platforms = _anyPlatform ? excludePlatforms : includePlatforms;
                    platforms.Clear();
                    platforms.AddRange(Platforms.All.Keys);
                }

                if (GUILayout.Button(Platforms.Button.Deselect.Label, _deselectButtonGUILayoutOption)) {
                    var platforms = _anyPlatform ? excludePlatforms : includePlatforms;
                    platforms.Clear();
                }
            }
        }

        public void OnDisable() {
            CleanupBackgroundTexture();

            _backgroundStyle = null;
            _selectButtonGUILayoutOption = null;
            _deselectButtonGUILayoutOption = null;
        }

        private void CleanupBackgroundTexture() {
            if (_backgroundTexture != null)
                UnityObject.DestroyImmediate(_backgroundTexture);
        }
    }
}