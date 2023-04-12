using System.Windows.Forms;

namespace Practice_3;

public partial class Form1 : Form
{
    private System.Windows.Forms.Timer moveTimer = new System.Windows.Forms.Timer();

    public List<Point> arPoints = new List<Point>(); //Список поставленных точек
    public List<Point> arOffsets = new List<Point>(); //Список 'смещенных' точек, используемые при передвижение
    public Dictionary<List<Point>, LineType> figures = new Dictionary<List<Point>, LineType>(); //Список сохраненных фигур с их типом линий
    public bool pointsFlag = true; //Флаг рисования точки 
    public bool dragFlag = false; //Флаг перемещения точки
    public bool moveFlag = false; //Флаг движения точек
    public bool saveFlag = false; //Флаг после сохранения фигуры
    public int iPointToDrag; // индекс перемещаемой точки

    public LineType LineTypeToShow; // линия соединения точек
    public int LineWidth { get; set; } = 5;       // толщина линии
    public enum LineType { None, Curve, Bezier, Polygone, FilledCurve }; // все виды линий

    public int pictureBoxWidth { get; set; }

    public Size PointSize { get; set; } = new Size(5, 5);
    public float PointRadius { get; set; } = 5;
    public Color PointColor { get; set; } = Color.Blue; //Цвет точки по умолчанию       
    public Color LineColor { get; set; } = Color.Blue; //Цвет линии по умолчанию
    public Form1()
    {
        InitializeComponent();

        Paint += Form1_Paint;
        MouseClick += Form1_MouseClick;

        MouseMove += Form1_MouseMove;
        MouseUp += new MouseEventHandler(Form1_MouseUp);
        MouseDown += new MouseEventHandler(Form1_MouseDown);

        KeyPreview = true;
        KeyDown += new KeyEventHandler(Form1_KeyDown);

        pictureBoxWidth = pictureBox1.Width;

        moveTimer.Interval = 5;
        moveTimer.Tick += new EventHandler(TimerTickHandler);

        button1.Click += new EventHandler(buttonPoints_Click);
        button2.Click += new EventHandler(buttonParametrs_Click);
        button3.Click += new EventHandler(buttonMove_Click);
        button4.Click += new EventHandler(buttonClear_Click);
        button5.Click += new EventHandler(buttonCurve_Click);
        button6.Click += new EventHandler(buttonPolygone_Click);
        button7.Click += new EventHandler(buttonBeziers_Click);
        button8.Click += new EventHandler(buttonFilledCurve_Click);
        button9.Click += new EventHandler(buttonSave_Click);

        //В начале блокируем некоторые кнопки
        button3.Enabled = false;
        button4.Enabled = false;
        button5.Enabled = false;
        button6.Enabled = false;
        button7.Enabled = false;
        button8.Enabled = false;
    }

    //Отрисовка
    void Form1_Paint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        if (arPoints.Count > 0)
        {
            ShowPoints(g, null);
            if (LineTypeToShow != LineType.None)
                ShowLine(g, LineTypeToShow, null);
            foreach (var figure in figures)
            {
                ShowPoints(g, figure.Key);
                ShowLine(g, figure.Value, figure.Key);
            }
        }

    }

    //Нажатие на кнопку мыши
    void Form1_MouseClick(object sender, MouseEventArgs e)
    {
        Point p;
        if (e.X > pictureBoxWidth && !dragFlag)
        {
            p = e.Location;
            if (pointsFlag)
            {
                arPoints.Add(p);
                LineTypeToShow = LineType.None;
                
                button7.Enabled = arPoints.Count > 3 && (arPoints.Count - 1) % 3 == 0 ? true : false;

                if (arPoints.Count >= 3)
                {
                    button5.Enabled = true;
                    button8.Enabled = true;
                }

                if(arPoints.Count >= 2)
                {
                    button6.Enabled = true;
                }

                button3.Enabled = true;
                button4.Enabled = true;
                Refresh();
            }
        }

        if (dragFlag) dragFlag = !dragFlag;
    }

    //Зажатие кнопки мыши
    void Form1_MouseMove(object sender, MouseEventArgs e)
    {
        if (dragFlag)
        {
            arPoints[iPointToDrag] = new Point(e.Location.X, e.Location.Y);
            Refresh();
        }
    }

    //Указатель на элементе
    void Form1_MouseUp(object sender, MouseEventArgs e)
    {
        dragFlag = false;
    }

    //Кнопка над элементом нажата
    void Form1_MouseDown(object sender, MouseEventArgs e)
    {
        for (int i = 0; i < arPoints.Count; i++)
        {
            if (IsOnPoint(arPoints[i], e.Location))
            {
                dragFlag = true;
                iPointToDrag = i;
                break;
            }
        }

    }

    //На точке или нет
    bool IsOnPoint(Point pPixel, Point pMouse)
    {
        if (pMouse.X >= pPixel.X - PointSize.Width &&
            pMouse.X <= pPixel.X + PointSize.Width &&
            pMouse.Y >= pPixel.Y - PointSize.Height &&
            pMouse.Y <= pPixel.Y + PointSize.Height)
            return true;
        else
            return false;

    }

    //Нажатие на кнопку на клавиатуре
    void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Escape: //Обработка Escape
                buttonClear_Click(sender, e);
                e.Handled = true;
                break;
            case Keys.Space: //Обработка Space
                buttonMove_Click(sender, e);
                e.Handled = true;
                break;
            case Keys.Oemplus: //Обработка +
                {
                    int _x = 0, _y = 0;
                    for (int i = 0; i < arOffsets.Count; i++)
                    {
                        if (arOffsets[i].X > 0)
                            _x = 1;
                        if (arOffsets[i].X < 0)
                            _x = -1;
                        if (arOffsets[i].Y > 0)
                            _y = 1;
                        if (arOffsets[i].Y < 0)
                            _y = -1;
                        arOffsets[i] = new Point(arOffsets[i].X + _x, arOffsets[i].Y + _y);
                    }
                    e.Handled = true;
                }
                break;
            case Keys.OemMinus: //Обработка -
                {
                    int _x = 0, _y = 0;
                    for (int i = 0; i < arOffsets.Count; i++)
                    {
                        if (arOffsets[i].X > 1)
                            _x = -1;
                        if (arOffsets[i].X < -1)
                            _x = 1;
                        if (arOffsets[i].Y > 1)
                            _y = -1;
                        if (arOffsets[i].Y < -1)
                            _y = 1;
                        arOffsets[i] = new Point(arOffsets[i].X + _x, arOffsets[i].Y + _y);
                    }
                    e.Handled = true;
                }
                break;
            default:
                break;
        }
    }

    //Отрисовка точек
    private void ShowPoints(Graphics g, List<Point>? figure)
    {
        if (figure == null)
        {
            foreach (var p in arPoints)
                g.FillEllipse(new SolidBrush(PointColor), p.X, p.Y, PointSize.Width, PointSize.Height);
        }
        else
        {
            foreach (var p in figure)
                g.FillEllipse(new SolidBrush(PointColor), p.X, p.Y, PointSize.Width, PointSize.Height);
        }
    }

    //Отрисовка линий
    void ShowLine(Graphics g, LineType line, List<Point>? figure)
    {
        SolidBrush brush = new SolidBrush(LineColor);
        Pen pen = new Pen(brush, LineWidth);
        if (figure == null)
        {
            switch (line)
            {
                case LineType.Bezier:
                    g.DrawBeziers(pen, arPoints.ToArray());
                    break;
                case LineType.Curve:
                    g.DrawClosedCurve(pen, arPoints.ToArray());
                    break;
                case LineType.FilledCurve:
                    g.FillClosedCurve(brush, arPoints.ToArray());
                    break;
                case LineType.Polygone:
                    g.DrawPolygon(pen, arPoints.ToArray());
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (line)
            {
                case LineType.Bezier:
                    g.DrawBeziers(pen, figure.ToArray());
                    break;
                case LineType.Curve:
                    g.DrawClosedCurve(pen, figure.ToArray());
                    break;
                case LineType.FilledCurve:
                    g.FillClosedCurve(brush, figure.ToArray());
                    break;
                case LineType.Polygone:
                    g.DrawPolygon(pen, figure.ToArray());
                    break;
                default:
                    break;
            }
        }
    }

    //Кнопка "Точки"
    void buttonPoints_Click(object sender, EventArgs e)
    {
        pointsFlag = !pointsFlag; //включение или отключение режима добавления точек по щелчку мыши
        dragFlag = false;
        moveFlag = false;
        if(!saveFlag)
        {
            saveFlag = !saveFlag;
            Refresh();
        } 
    }

    //Кнопка "Параметры"
    void buttonParametrs_Click(object sender, EventArgs e)
    {
        ParamForm paramsForm = new ParamForm(this);
        paramsForm.Show(); //открытие окна с параметрами
    }

    //Кнопка "Движение"
    void buttonMove_Click(object sender, EventArgs e)
    {
        dragFlag = false;
        
        button3.Enabled = true;
        moveFlag = !moveFlag;

        if (moveFlag)
        {
            arOffsets = new List<Point>();
            int _x, _y;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            _x = rnd.Next(1, 5);
            _y = rnd.Next(1, 5);
            for (int i = 0; i < arPoints.Count; i++)
            {
                arOffsets.Add(new Point(_x, _y));
            }
            moveTimer.Start();
        }
        else
            moveTimer.Stop();
    }

    //Перегрузка метода для обработки управляющих кнопок
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        bool IsHandled = true;
        switch (keyData)
        {
            case Keys.Up:
                if (!moveFlag)
                {
                    for (int i = 0; i < arPoints.Count; i++)
                    {
                        if (arPoints.Min(a => a.Y) == 0)
                            break;
                        arPoints[i] = new Point(arPoints[i].X, arPoints[i].Y - 1);
                    }
                    Refresh();
                }
                break;
            case Keys.Down:
                if (!moveFlag)
                {
                    for (int i = 0; i < arPoints.Count; i++)
                    {
                        if (arPoints.Max(a => a.Y) == this.ClientRectangle.Height - LineWidth)
                            break;
                        arPoints[i] = new Point(arPoints[i].X, arPoints[i].Y + 1);
                    }
                    Refresh();
                }
                break;
            case Keys.Left:
                if (!moveFlag)
                {
                    for (int i = 0; i < arPoints.Count; i++)
                    {
                        if (arPoints.Min(a => a.X) == pictureBoxWidth)
                            break;
                        arPoints[i] = new Point(arPoints[i].X - 1, arPoints[i].Y);
                    }
                    Refresh();
                }
                break;
            case Keys.Right:
                if (!moveFlag)
                {
                    for (int i = 0; i < arPoints.Count; i++)
                    {
                        if (arPoints.Max(a => a.X) == this.ClientRectangle.Width - LineWidth)
                            break;
                        arPoints[i] = new Point(arPoints[i].X + 1, arPoints[i].Y);
                    }
                    Refresh();
                }
                break;
            default:
                IsHandled = false;
                break;
        }
        return IsHandled;
    }

    void TimerTickHandler(object sender, EventArgs e)
    {
        MovePoints();
        Refresh();
    }

    //Алгоритм передвижение точек
    void MovePoints()
    {
        int _x, _y;
        if (pointsFlag)
            button1.PerformClick();
        for (int i = 0; i < arPoints.Count; i++)
        {
            _x = arPoints[i].X + arOffsets[i].X;

            //Проверка на то, чтобы точки не вылетали за пределы поля по горизонтали
            if (_x >= this.ClientRectangle.Width || _x <= pictureBoxWidth)
            {
                arOffsets[i] = new Point(-arOffsets[i].X, arOffsets[i].Y);
                _x = arPoints[i].X + arOffsets[i].X;
            }

            _y = arPoints[i].Y + arOffsets[i].Y;

            //Проверка на то, чтобы точки не вылетали за пределы поля по вертикали
            if (_y >= this.ClientRectangle.Height || _y <= 0)
            {
                arOffsets[i] = new Point(arOffsets[i].X, -arOffsets[i].Y);
                _y = arPoints[i].Y + arOffsets[i].Y;
            }
            arPoints[i] = new Point(_x, _y);
        }
    }

    //Кнопка "Очистить"
    void buttonClear_Click(object sender, EventArgs e)
    {
        moveTimer.Stop();
        pointsFlag = false;
        dragFlag = false;
        moveFlag = false;
        arPoints.Clear();
        arOffsets.Clear();
        figures.Clear();
        PointColor = Color.Blue;
        LineColor = Color.Blue;
        LineWidth = 5;
        LineTypeToShow = LineType.None;
        button3.Enabled = false;
        button4.Enabled = false;
        button5.Enabled = false;
        button6.Enabled = false;
        button7.Enabled = false;
        button8.Enabled = false;
        Refresh();
    }

    //Кнопка "Кривая"
    void buttonCurve_Click(object sender, EventArgs e)
    {
        LineTypeToShow = LineTypeToShow != LineType.Curve ? LineType.Curve : LineType.None;

        if (pointsFlag)
            button1.PerformClick();
        Refresh();
    }

    //Кнопка "Ломанная"
    void buttonPolygone_Click(object sender, EventArgs e)
    {
        LineTypeToShow = LineTypeToShow != LineType.Polygone ? LineType.Polygone : LineType.None;

        if (pointsFlag)
            button1.PerformClick();
        Refresh();
    }

    //Кнопка "Безье"
    void buttonBeziers_Click(object sender, EventArgs e)
    {
        LineTypeToShow = LineTypeToShow != LineType.Bezier ? LineType.Bezier : LineType.None;

        if (pointsFlag)
            button1.PerformClick();
        Refresh();
    }

    //Кнопка "Закрашенная"
    void buttonFilledCurve_Click(object sender, EventArgs e)
    {
        LineTypeToShow = LineTypeToShow != LineType.FilledCurve ? LineType.FilledCurve : LineType.None;

        if (pointsFlag)
            button1.PerformClick();
        Refresh();
    }

    //Кнопка "Сохранение"
    void buttonSave_Click(object sender, EventArgs e)
    {
        figures.Add(new List<Point>(arPoints), LineTypeToShow);

        if (pointsFlag)
            button1.PerformClick();
        arPoints.Clear();

        saveFlag = true;
    }
}
