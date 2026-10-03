Public Class frmPopUp_img2
    Private oGambar As Image
    Private down As Boolean = False

    Public Sub fn_LoadMe(ByVal sGambar As Image)
        oGambar = sGambar
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        sPicture = picIMAGE.Image
        sFind10 = "X"

        Me.Close()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Public Sub fn_LoadLanguage()

    End Sub
    Private Sub fn_Load()
        picIMAGE.Image = oGambar
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        sFind10 = String.Empty
        sPicture = Nothing
        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
    'Private Sub picGAMBAR2_MouseDown(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseDown
    '    down = True
    'End Sub
    'Private Sub picGAMBAR2_MouseUp(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseUp
    '    down = False
    'End Sub
    'Private Sub picGAMBAR2_MouseMove(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseMove
    '    If down = True Then
    '        Dim ImageToDrawOn As Image
    '        Dim g As Graphics
    '        Dim Brush1 As New SolidBrush(Color.Black)
    '        g = picGAMBAR2.CreateGraphics()
    '        ImageToDrawOn = picGAMBAR2.Image
    '        g = Graphics.FromImage(ImageToDrawOn)
    '        g.FillEllipse(Brush1, e.X, e.Y, 5, 5)
    '        picGAMBAR2.Image = ImageToDrawOn
    '        g.Dispose()
    '        Brush1.Dispose()
    '    End If

    'End Sub
    Private _Previous As System.Nullable(Of Point) = Nothing
    Private Sub pictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseDown
        _Previous = e.Location
        pictureBox1_MouseMove(sender, e)
    End Sub

    Private Sub pictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseMove
        If _Previous IsNot Nothing Then
            Dim GridColor As Color = Color.Red
            Dim GridPen As New Pen(GridColor)
            GridPen.Width = 2

            Using g As Graphics = Graphics.FromImage(picIMAGE.Image)
                g.DrawLine(GridPen, _Previous.Value, e.Location)
                'g.DrawString("TEst", RichTextBox1.Font, Brushes.Black, New PointF(10, 10))

            End Using
            picIMAGE.Invalidate()
            _Previous = e.Location
        End If
    End Sub

    Private Sub pictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseUp
        _Previous = Nothing
    End Sub
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        picIMAGE.Image = CType(My.Resources.ResourceManager.GetObject("Capture_1"), Image)
        'ResizeBitmap("C:\somefolder\somebitmap.bmp", 500, 500)
    End Sub
    'Function ResizeBitmap(ByVal bitmapToResize As Bitmap, ByVal width As Integer, ByVal height As Integer)
    '    'make a blank bitmap the correct size
    '    Dim NewBitmap As New Bitmap(width, height)
    '    'make an instance of graphics that will draw on "NewBitmap"
    '    Dim BitmpGraphics As Graphics = Graphics.FromImage(NewBitmap)
    '    'work out the scale factor
    '    Dim scaleFactorX As Integer = bitmapToResize.Width / width
    '    Dim scaleFactorY As Integer = bitmapToResize.Height / width
    '    'resize the graphics
    '    BitmpGraphics.ScaleTransform(scaleFactorX, scaleFactorY)
    '    'draw the bitmap to NewBitmap
    '    BitmpGraphics.DrawImage(bitmapToResize, 0, 0)
    '    Return NewBitmap
    'End Function

#End Region
End Class