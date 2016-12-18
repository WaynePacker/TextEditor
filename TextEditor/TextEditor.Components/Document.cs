using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Base;
using TextEditor.Interfaces;

namespace TextEditor.Components
{
    public class Document : AViewModel, IDocument
    {
        private string content;
        private bool hasChanges;

        public Document(string filePath)
        {
            this.FilePath = filePath;
            this.Content = System.IO.File.ReadAllText(filePath);
            this.Name = filePath;
        }

        public Document()
        {
            this.FilePath = null;
            this.Content = "";
            this.Name = "New File";
        }

        
        public string Content
        {
            get { return content; }
            set
            {
                this.content = value;
                RaisePropertyChanged(nameof(Content));
                this.HasChanges = true;
            }
        }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (value)
                    this.hasChanges = value;
                
                RaisePropertyChanged(nameof(HasChanges));
            }
        }
    }
}
