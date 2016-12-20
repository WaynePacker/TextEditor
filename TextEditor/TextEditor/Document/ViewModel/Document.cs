using System;
using System.IO;
using TextEditor.Base;
using TextEditor.Interfaces;

namespace TextEditor.Components
{
    public class Document : ANotifyBase, IDocument
    {
        private string content;
        private bool hasChanges;

        public Document(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                var name = Path.GetFileName(filePath);
                InitDocument(filePath, content, name);
            }
            catch(Exception)
            {
                throw new Exception("Cannot read file");
            }
                
        }

        public Document()
        {
            InitDocument(null, "", "New File");
        }

        private void InitDocument(string filePath, string content, string name)
        {
            this.FilePath = filePath;
            this.Content = content;
            this.Name = name;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        
        public string Content
        {
            get { return content; }
            set
            {
                SetAndRaisePropertyChanged(ref this.content, value);
                if(value.Length >= 1)
                    this.HasChanges = true;
            }
        }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public bool HasChanges
        {
            get { return hasChanges; }
            set { SetAndRaisePropertyChanged(ref this.hasChanges, value); }
        }
    }
}
