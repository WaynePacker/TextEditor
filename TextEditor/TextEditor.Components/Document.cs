using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Interfaces;

namespace TextEditor.Components
{
    public class Document : IDocument
    {
        public Document(string filePath)
        {
            this.FilePath = filePath;
            this.Content = System.IO.File.ReadAllText(filePath);
            this.Name = filePath;
        }

        public string Content { get; set; }

        public string FilePath { get; set; }

        public string Name { get; set; }

    }
}
