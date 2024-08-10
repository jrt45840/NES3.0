Imports System.Text.RegularExpressions

Public Class Form1
    Dim N3 As New NES3FileHandler

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.Items.AddRange(New String() {"Asia", "Australia", "Brazil", "Canada", "China", "Europe", "France", "Germany", "Italy", "Japan", "Korea", "Russia", "Spain", "Sweden", "United Kingdom", "USA", "World", "Other"})
        ComboBox2.Items.AddRange(New String() {"Rev 1", "Rev 2", "Rev 3", "Rev 4", "Rev 5", "Rev 6", "Rev 7", "Rev 8", "Rev 9", "Rev 10"})
        ComboBox3.Items.AddRange(New String() {"Beta 1", "Beta 2", "Beta 3", "Beta 4", "Beta 5", "Beta 6", "Beta 7", "Beta 8", "Beta 9", "Beta 10"})
        ComboBox4.Items.AddRange(New String() {"Proto 1", "Proto 2", "Proto 3", "Proto 4", "Proto 5", "Proto 6", "Proto 7", "Proto 8", "Proto 9", "Proto 10"})
        ComboBox5.Items.AddRange(New String() {"Demo 1", "Demo 2", "Demo 3", "Demo 4", "Demo 5", "Demo 6", "Demo 7", "Demo 8", "Demo 9", "Demo 10"})
        ComboBox6.Items.AddRange(New String() {"Licensed", "Unlicensed"})
        ComboBox7.Items.AddRange(New String() {"Yes", "No"})
        ComboBox8.Items.AddRange(New String() {"Yes", "No"})

        ' Set file filters
        OpenFileDialog1.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
        OpenFileDialog2.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
        OpenFileDialog3.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
        OpenFileDialog4.Filter = "NES Files (*.nes)|*.nes"
        OpenFileDialog5.Filter = "NES 3.0 Files (*.nes3)|*.nes3"

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            PictureBox1.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If OpenFileDialog2.ShowDialog() = DialogResult.OK Then
            PictureBox2.BackgroundImage = Image.FromFile(OpenFileDialog2.FileName)
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If OpenFileDialog3.ShowDialog() = DialogResult.OK Then
            PictureBox3.BackgroundImage = Image.FromFile(OpenFileDialog3.FileName)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog4.ShowDialog() = DialogResult.OK Then
            TextBox1.Text = OpenFileDialog4.FileName

            Dim startPos As Integer = TextBox1.Text.LastIndexOf("\"c) + 1 ' Start after the last backslash
            Dim endPos As Integer = TextBox1.Text.IndexOf("("c) ' Position of the first [

            ' Extract the game title
            Try
                Dim gameTitle As String = TextBox1.Text.Substring(startPos, endPos - startPos).Trim()
                TextBox2.Text = gameTitle
            Catch
            End Try


            ' Extract and set metadata based on file name
            If Strings.InStrRev(TextBox1.Text, "Asia") Then ComboBox1.Text = "Asia"
            If Strings.InStrRev(TextBox1.Text, "Australia") Then ComboBox1.Text = "Australia"
            If Strings.InStrRev(TextBox1.Text, "Brazil") Then ComboBox1.Text = "Brazil"
            If Strings.InStrRev(TextBox1.Text, "Canada") Then ComboBox1.Text = "Canada"
            If Strings.InStrRev(TextBox1.Text, "China") Then ComboBox1.Text = "China"
            If Strings.InStrRev(TextBox1.Text, "Europe") Then ComboBox1.Text = "Europe"
            If Strings.InStrRev(TextBox1.Text, "France") Then ComboBox1.Text = "France"
            If Strings.InStrRev(TextBox1.Text, "Germany") Then ComboBox1.Text = "Germany"
            If Strings.InStrRev(TextBox1.Text, "Italy") Then ComboBox1.Text = "Italy"
            If Strings.InStrRev(TextBox1.Text, "Japan") Then ComboBox1.Text = "Japan"
            If Strings.InStrRev(TextBox1.Text, "Korea") Then ComboBox1.Text = "Korea"
            If Strings.InStrRev(TextBox1.Text, "Russia") Then ComboBox1.Text = "Russia"
            If Strings.InStrRev(TextBox1.Text, "Spain") Then ComboBox1.Text = "Spain"
            If Strings.InStrRev(TextBox1.Text, "Sweden") Then ComboBox1.Text = "Sweden"
            If Strings.InStrRev(TextBox1.Text, "United Kingdom") Then ComboBox1.Text = "United Kingdom"
            If Strings.InStrRev(TextBox1.Text, "USA") Then ComboBox1.Text = "USA"
            If Strings.InStrRev(TextBox1.Text, "World") Then ComboBox1.Text = "World"
            If Strings.InStrRev(TextBox1.Text, "Other") Then ComboBox1.Text = "Other"

            ' Regular expression to find (Rev #)
            Dim revPattern1 As String = "\(Rev (\d+)\)"
            Dim match1 As Match = Regex.Match(TextBox1.Text, revPattern1)
            If match1.Success Then
                ' Extract revision number from the match
                Dim revision As String = "Rev " & match1.Groups(1).Value
                ComboBox2.Text = revision
            End If

            ' Regular expression to find (Beta #)
            Dim revPattern2 As String = "\(Beta (\d+)\)"
            Dim match2 As Match = Regex.Match(TextBox1.Text, revPattern2)
            If match2.Success Then
                ' Extract revision number from the match
                Dim Beta As String = "Beta " & match2.Groups(1).Value
                ComboBox3.Text = Beta
            End If

            ' Regular expression to find (Proto #)
            Dim revPattern3 As String = "\(Proto (\d+)\)"
            Dim match3 As Match = Regex.Match(TextBox1.Text, revPattern3)
            If match3.Success Then
                ' Extract revision number from the match
                Dim Proto As String = "Proto " & match3.Groups(1).Value
                ComboBox4.Text = Proto
            End If

            ' Regular expression to find (Demo #)
            Dim revPattern4 As String = "\(Demo (\d+)\)"
            Dim match4 As Match = Regex.Match(TextBox1.Text, revPattern4)
            If match4.Success Then
                ' Extract revision number from the match
                Dim Demo As String = "Demo " & match4.Groups(1).Value
                ComboBox5.Text = Demo
            End If

            If Strings.InStrRev(TextBox1.Text, "Unl") Then ComboBox6.Text = "Unlicensed"
            If Strings.InStrRev(TextBox1.Text, "Aftermarket") Then ComboBox7.Text = "Yes"
            If Strings.InStrRev(TextBox1.Text, "Pirate") Then ComboBox8.Text = "Yes"

            Dim extractedText As String = GetTextBetweenBrackets(TextBox1.Text)
            Dim cleanedText As String = RemovePrefix(extractedText, "T-En by ")
            TextBox3.Text = cleanedText

        End If
    End Sub

    Private Function GetTextBetweenBrackets(ByVal input As String) As String
        ' Regular expression to find text between square brackets
        Dim pattern As String = "\[(.*?)\]"
        Dim match As Match = Regex.Match(input, pattern)
        If match.Success Then
            ' Extract text between brackets
            Return match.Groups(1).Value
        End If
        Return String.Empty
    End Function

    Private Function RemovePrefix(ByVal text As String, ByVal prefix As String) As String
        ' Check if the text starts with the prefix and remove it
        If text.StartsWith(prefix) Then
            Return text.Substring(prefix.Length).Trim()
        End If
        Return text
    End Function


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If OpenFileDialog5.ShowDialog() = DialogResult.OK Then
            Dim nes3 As NES3FileHandler.NESMetadata = N3.ReadNES3File(OpenFileDialog5.FileName)

            TextBox2.Text = nes3.Title
            ComboBox1.Text = nes3.Region
            ComboBox2.Text = nes3.Revision
            ComboBox3.Text = nes3.Beta
            ComboBox4.Text = nes3.Proto
            ComboBox5.Text = nes3.Demo
            ComboBox6.Text = nes3.Licensed
            ComboBox7.Text = nes3.Aftermarket
            ComboBox8.Text = nes3.Pirate
            TextBox3.Text = nes3.Translation
            TextBox4.Text = nes3.Description
            PictureBox1.BackgroundImage = If(Not String.IsNullOrEmpty(nes3.FrontCoverBase64), N3.ConvertBase64ToImage(nes3.FrontCoverBase64), Nothing)
            PictureBox2.BackgroundImage = If(Not String.IsNullOrEmpty(nes3.BackCoverBase64), N3.ConvertBase64ToImage(nes3.BackCoverBase64), Nothing)
            PictureBox3.BackgroundImage = If(Not String.IsNullOrEmpty(nes3.CartridgeBase64), N3.ConvertBase64ToImage(nes3.CartridgeBase64), Nothing)

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim metadata As New NES3FileHandler.NESMetadata With {
            .Title = TextBox2.Text,
            .Region = ComboBox1.Text,
            .Revision = ComboBox2.Text,
            .Beta = ComboBox3.Text,
            .Proto = ComboBox4.Text,
            .Demo = ComboBox5.Text,
            .Licensed = ComboBox6.Text,
            .Aftermarket = ComboBox7.Text,
            .Pirate = ComboBox8.Text,
            .Translation = TextBox3.Text,
            .Description = TextBox4.Text,
        .FrontCoverBase64 = If(PictureBox1.BackgroundImage IsNot Nothing, N3.ConvertImageToBase64(PictureBox1.BackgroundImage), ""),
        .BackCoverBase64 = If(PictureBox2.BackgroundImage IsNot Nothing, N3.ConvertImageToBase64(PictureBox2.BackgroundImage), ""),
        .CartridgeBase64 = If(PictureBox3.BackgroundImage IsNot Nothing, N3.ConvertImageToBase64(PictureBox3.BackgroundImage), "")
        }

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            N3.AddtoNESFile(OpenFileDialog4.FileName, SaveFileDialog1.FileName, metadata)
        End If
    End Sub

End Class
