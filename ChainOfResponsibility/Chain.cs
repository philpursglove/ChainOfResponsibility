using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.Composition;
using System.Reflection;

namespace ChainOfResponsibility
{
    public class Chain<T> where T : Element
    {
        [ImportMany]
        public IEnumerable<T> Elements;

        public void Configure()
        {
            CheckChainIsValid();

            if (Elements.Any())
            {
                foreach (Element element in Elements)
                {
                    Type type = element.GetType();
                    TypeInfo info = type.GetTypeInfo();

                    if (Attribute.IsDefined(info, typeof(Successor)))
                    {
                        Successor successor = (Successor)type.GetCustomAttribute(typeof(Successor));

                        element.setSuccessor(Elements.First(r => r.GetType().FullName == successor.TypeName));
                    }
                }
            }
        }

        private void CheckChainIsValid()
        {
            List<Element> firsts = new List<Element>(Elements.Count());

            foreach (var element in Elements)
            {
                Type type = element.GetType();
                if (Attribute.IsDefined(type.GetTypeInfo(), typeof(First)))
                {
                    firsts.Add(element);
                }
            }

            switch (firsts.Count)
            {
                case 0:
                    throw new NoFirstElementException("One element must be decorated with the [First] attribute");
                case 1:
                    break;
                default:
                    throw new TooManyFirstsException("Too many elements decorated with the [First] attribute");
            }
        }

        public T First
        {
            get
            {
                foreach (var element in Elements)
                {
                    TypeInfo info = element.GetType().GetTypeInfo();
                    if (Attribute.IsDefined(info, typeof(First)))
                    {
                        return element;
                    }
                }
                return null;
            }
            private set { }
        }

    }
}
