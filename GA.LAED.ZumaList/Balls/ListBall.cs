using System;

namespace GA.LAED.ZumaList.Balls {

    public class Element {
        public Ball Ball { get; private set; }
        public Element Next { get; set; }
        public Element Before { get; set; }

        public Element(Ball ball) {
            this.Ball = ball;
            this.Next = this.Before = null;
        }
    }

    public class ListBall {

        private readonly int START_LENGTH;

        private int TotalCount { get; set; }

        private Element First { get; set; }
        private Element Last { get; set; }
        public int Count { get; private set; }
        public int Max { get; private set; }

        public ListBall(int max, int start) {
            this.Max = max;
            this.START_LENGTH = start;
            this.First = this.Last = null;
            this.TotalCount = 0;

            for (int i = 0; i < START_LENGTH; i++) {
                this.AddNewBall();
            }
        }

        public void AddNewBall() {
            this.TotalCount++;
            Ball ball = new Ball(this.TotalCount);
            this.InsertFinal(ball);
        }

        public void InsertAt(Ball ball, int position) {
            if (position > this.Count || position < 1)
                return;

            Element newElement = new Element(ball);
            Element element = this.First;
            Element elementBefore = element;

            int count = 1;

            while (element != null) {
                if (count == position) {
                    if (position == 1) {
                        newElement.Next = this.First;
                        this.First.Before = newElement;
                        this.First = newElement;
                    } else {
                        elementBefore.Next = newElement;
                        newElement.Before = elementBefore;
                        element.Before = newElement;
                        newElement.Next = element;
                    }
                    this.Count++;
                    break;
                }

                elementBefore = element;
                element = element.Next;
                count++;
            }
        }

        private void InsertFinal(Ball ball) {
            Element element = new Element(ball);
            if (this.Empty()) {
                this.First = element;
                this.Last = this.First;
            } else {
                this.Last.Next = element;
                element.Before = this.Last;
                this.Last = element;
            }
            this.Count++;
        }

        public int RemoveGroup(int position, Color color) {
            int removed = 0;
            int count = 1;

            int start = -1, end = -1;
            Element elementeStart = null, elementEnd = null;

            bool positionFind = false, colorFind = false, remove = false;


            Element element = this.First;
            while (element != null) {

                if (count == position) {
                    positionFind = true;
                }

                if (element.Ball.Color == color && !colorFind) {
                    start = count;
                    elementeStart = element;
                    colorFind = true;
                } else if (element.Ball.Color != color && !positionFind) {
                    start = end = -1;
                    elementeStart = elementEnd = null;
                    colorFind = false;
                } else if (element.Ball.Color != color && colorFind && positionFind) {
                    end = count;
                    elementEnd = element.Before;
                    remove = true;
                    break;
                } else if (element.Next == null && colorFind && positionFind) {
                    end = count + 1;
                    elementEnd = element;
                    remove = true;
                    break;
                }

                count++;
                element = element.Next;
            }

            if (remove) {

                removed = end - start;

                if (removed >= 3) {
                    if (start == 1) {
                        this.First = elementEnd.Next;
                        this.First.Before = null;
                    } else if (end == this.Count + 1) {
                        this.Last = elementeStart.Before;
                        this.Last.Next = null;
                    } else {
                        elementeStart.Before.Next = elementEnd.Next;
                        elementEnd.Next.Before = elementeStart.Before;
                    }
                    this.Count -= removed;
                } else {
                    removed = 0;
                }
            }


            return removed;
        }

        public Ball RemoveAt(int position) {
            if (position > this.Count || position < 1)
                return null;

            Element element = this.First;
            int count = 1;
            while (element != null) {

                if (count == position) {
                    if (count == 1) {
                        this.First = element.Next;
                        this.First.Before = null;
                    } else if (count == this.Count) {
                        this.Last = element.Before;
                        this.Last.Next = null;
                    } else {
                        element.Before.Next = element.Next;
                        element.Next.Before = element.Before;
                    }
                    this.Count--;
                    return element.Ball;
                }

                element = element.Next;
                count++;
            }

            return null;
        }

        public bool Empty() {
            return this.First == null && this.Last == this.First;
        }

        public bool Full() {
            return this.Count >= this.Max;
        }

        public void Print() {
            Element element = this.First;
            while (element != null) {
                element.Ball.Print();
                element = element.Next;
            }

            ConsoleGame.NewLine();
            element = this.First;
            int count = 1;
            while (element != null) {
                Console.Write($"{count,-2} ");
                count++;
                element = element.Next;
            }
        }
    }
}
