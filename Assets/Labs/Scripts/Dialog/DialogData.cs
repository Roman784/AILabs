using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gameplay
{
    [XmlRoot("dialog")]
    [Serializable]
    public class Dialog
    {
        [XmlAttribute("id")]
        public string ID;

        [XmlElement("node")]
        public List<Node> Nodes = new List<Node>();
    }

    public class Node
    {
        [XmlAttribute("id")]
        public string ID;

        [XmlAttribute("nextNode")]
        public string NextNodeID;

        [XmlElement("text")]
        public TextLine DialogueText;

        [XmlArray("responses")]
        [XmlArrayItem("response")]
        public List<Response> Responses;

        public bool HasResponses => Responses != null && Responses.Count > 0;
    }

    [Serializable]
    public class TextLine
    {
        [XmlText]
        public string Text;
    }

    [Serializable]
    public class Response
    {
        [XmlAttribute("text")]
        public string Text;

        [XmlAttribute("nextNode")]
        public string NextNodeID;
    }
}
