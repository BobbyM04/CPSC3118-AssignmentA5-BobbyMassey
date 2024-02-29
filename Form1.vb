'Bobby Massey - Assignment A5, 02/29/24, Graphical User Interface Dev.: CPSC 3118
Public Class Form1
    'Constants for badge costs
    Const ConventionSuperHeroCost As Decimal = 380
    Const ConventionAutographsCost As Decimal = 275
    Const ConventionCost As Decimal = 209
    Public Property txtGroupSize As TextBox
    Public Property txtRegistrationCost As TextBox
    Public Property radioConventionOnly As RadioButton
    Public Property radioConventionSuperhero As RadioButton
    Public Property radioConventionAutographs As RadioButton
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set background image
        Dim picComic As New PictureBox()
        picComic.SizeMode = PictureBoxSizeMode.StretchImage
        picComic.Dock = DockStyle.Top
        picComic.Height = 200
        Try
            picComic.Image = Image.FromFile("C:\Users\Bobby\comic.jpg")
        Catch ex As Exception
            MessageBox.Show("Error loading payroll image: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Controls.Add(picComic)

        'Label for Comic Convention Registration
        Dim lblTitle As New Label
        lblTitle.Text = "Comic Convention Registration"
        lblTitle.Font = New Font("Comic Sans MS", 20, FontStyle.Bold)
        lblTitle.ForeColor = Color.Orange
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        lblTitle.AutoSize = False
        lblTitle.Size = New Size(Me.ClientSize.Width, 50)
        lblTitle.Location = New Point(0, picComic.Bottom + 10)
        Me.Controls.Add(lblTitle)

        ' Group Size Label and TextBox
        Dim lblGroupSize As New Label
        lblGroupSize.Text = "Group Size:"
        lblGroupSize.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
        lblGroupSize.ForeColor = Color.Orange
        lblGroupSize.AutoSize = True
        lblGroupSize.Top = lblTitle.Bottom + 20
        lblGroupSize.Left = 240
        Me.Controls.Add(lblGroupSize)

        txtGroupSize = New TextBox
        txtGroupSize.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
        txtGroupSize.Left = lblGroupSize.Right + 10
        txtGroupSize.Top = lblGroupSize.Top - 3
        txtGroupSize.Width = 50
        Me.Controls.Add(txtGroupSize)

        ' Badge Type Panel (Light Blue Box)
        Dim badgeTypePanel As New Panel
        badgeTypePanel.BackColor = Color.LightBlue
        badgeTypePanel.Size = New Size(350, 150)
        badgeTypePanel.Location = New Point(200, lblGroupSize.Bottom + 20)
        Me.Controls.Add(badgeTypePanel)

        'Badge Type Radio Buttons
        Dim lblBadgeTypePanel As New Label
        lblBadgeTypePanel.Text = "Select Badge Type:"
        lblBadgeTypePanel.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
        lblBadgeTypePanel.ForeColor = Color.Orange
        lblBadgeTypePanel.AutoSize = True
        lblBadgeTypePanel.Location = New Point(10, 10)
        badgeTypePanel.Controls.Add(lblBadgeTypePanel)

        radioConventionSuperhero = New RadioButton
        radioConventionSuperhero.Text = "Convention + Superhero Experience"
        radioConventionSuperhero.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        radioConventionSuperhero.ForeColor = Color.Orange
        radioConventionSuperhero.AutoSize = True
        radioConventionSuperhero.Location = New Point(10, lblBadgeTypePanel.Bottom + 10)
        badgeTypePanel.Controls.Add(radioConventionSuperhero)

        radioConventionAutographs = New RadioButton
        radioConventionAutographs.Text = "Convention + Autographs"
        radioConventionAutographs.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        radioConventionAutographs.ForeColor = Color.Orange
        radioConventionAutographs.AutoSize = True
        radioConventionAutographs.Location = New Point(10, radioConventionSuperhero.Bottom + 5)
        badgeTypePanel.Controls.Add(radioConventionAutographs)

        radioConventionOnly = New RadioButton
        radioConventionOnly.Text = "Convention"
        radioConventionOnly.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        radioConventionOnly.ForeColor = Color.Orange
        radioConventionOnly.AutoSize = True
        radioConventionOnly.Location = New Point(10, radioConventionAutographs.Bottom + 5)
        radioConventionOnly.Checked = True 'Preselecting this option
        badgeTypePanel.Controls.Add(radioConventionOnly)

        'Registration Cost Label and TextBox
        Dim lblRegistrationCost As New Label
        lblRegistrationCost.Text = "Registration Cost:"
        lblRegistrationCost.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
        lblRegistrationCost.ForeColor = Color.Orange
        lblRegistrationCost.AutoSize = True
        lblRegistrationCost.Location = New Point(200, badgeTypePanel.Bottom + 20)
        Me.Controls.Add(lblRegistrationCost)

        txtRegistrationCost = New TextBox
        txtRegistrationCost.Font = New Font("Comic Sans MS", 12, FontStyle.Bold)
        txtRegistrationCost.ReadOnly = True
        txtRegistrationCost.BackColor = Color.Orange
        txtRegistrationCost.BorderStyle = BorderStyle.None
        txtRegistrationCost.TextAlign = HorizontalAlignment.Center
        txtRegistrationCost.Top = lblRegistrationCost.Top - 3
        txtRegistrationCost.Left = lblRegistrationCost.Right + 10
        txtRegistrationCost.Width = 120
        Me.Controls.Add(txtRegistrationCost)

        'Buttons
        Dim btnCalculate As New Button
        btnCalculate.Text = "Calculate"
        btnCalculate.Size = New Size(100, 30)
        btnCalculate.Location = New Point((Me.ClientSize.Width - 300) \ 2, lblRegistrationCost.Bottom + 20)
        btnCalculate.BackColor = Color.LightBlue
        btnCalculate.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        AddHandler btnCalculate.Click, AddressOf btnCalculate_Click
        Me.Controls.Add(btnCalculate)

        Dim btnClear As New Button
        btnClear.Text = "Clear"
        btnClear.Size = New Size(100, 30)
        btnClear.Location = New Point(btnCalculate.Right + 20, lblRegistrationCost.Bottom + 20)
        btnClear.BackColor = Color.LightBlue
        btnClear.Font = New Font("Comic Sans MS", 10, FontStyle.Bold)
        AddHandler btnClear.Click, AddressOf btnClear_Click
        Me.Controls.Add(btnClear)
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        'Clear the TextBoxes and select the first radio button
        txtGroupSize.Clear()
        txtRegistrationCost.Clear()
        txtGroupSize.Focus()
        radioConventionOnly.Checked = True
    End Sub
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs)
        'Variable to hold the total cost
        Dim totalCost As Decimal = 0

        'Validation for group size
        Dim groupSize As Integer
        If Not Integer.TryParse(txtGroupSize.Text, groupSize) Then
            MessageBox.Show("Please enter a valid numeric group size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If groupSize < 1 Or groupSize > 20 Then
            MessageBox.Show("Group size must be between 1 and 20.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'Ensure that the constants are properly initialized
        If ConventionSuperHeroCost = 0 Then
            MessageBox.Show("ConventionSuperHeroCost is not properly initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If ConventionAutographsCost = 0 Then
            MessageBox.Show("ConventionAutographsCost is not properly initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If ConventionCost = 0 Then
            MessageBox.Show("ConventionCost is not properly initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        'Determine selected badge type and calculate total cost
        If radioConventionSuperhero.Checked Then
            totalCost = groupSize * ConventionSuperHeroCost
        ElseIf radioConventionAutographs.Checked Then
            totalCost = groupSize * ConventionAutographsCost
        ElseIf radioConventionOnly.Checked Then
            totalCost = groupSize * ConventionCost
        End If
        'Display total cost
        txtRegistrationCost.Text = totalCost.ToString("C")
    End Sub
End Class
