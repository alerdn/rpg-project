using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();

        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        // Anota��o para rodar essa fun��o apenas no modo edi��o da Unity
#if UNITY_EDITOR
        private void Awake()
        {
            if (nodes.Count == 0)
            {
                DialogueNode rootNode = new DialogueNode
                {
                    uniqueID = Guid.NewGuid().ToString()
                };
                nodes.Add(rootNode);
            }

            OnValidate();
        }
#endif

        /* OnValidate � como o Awake, ele executa quando carrega o ScriptableObject, mas tamb�m quando ele � editado
         * Temos que chamar essa fun��o manualmente no Awake() porque ela s� � executada no Inspector
         * ent�o o dicion�rio n�o seria criado quando f�ssemos jogar o jogo
         */
        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.children)
            {
                if (nodeLookup.ContainsKey(childID))
                    yield return nodeLookup[childID];
            }
        }

        public void CreateNode(DialogueNode parent)
        {
            DialogueNode childNode = new DialogueNode { uniqueID = Guid.NewGuid().ToString() };
            parent.children.Add(childNode.uniqueID);
            nodes.Add(childNode);
            OnValidate();
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            CleanDanglingChildren(nodeToDelete);
        }

        private void CleanDanglingChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.children.Remove(nodeToDelete.uniqueID);
            }
        }
    }
}

