Module Module1


    Sub Main()

        Module1.Initialization()

        Console.WriteLine("Please Enter Your Requested Dimension")
        Dim Dimension As Integer = Console.ReadLine() - 1
        If Dimension > 56 Or Dimension < 1 Then
            Console.WriteLine("Error: ArgumentOutOfRangeException")
            Dimension = 56
            Console.WriteLine("Dimension Has Been Set To The Biggest Integer")
        End If
        Console.WriteLine()

        Console.WriteLine("Please Enter Your Requested Decay Constant")
        Dim DecayConstant As Double = Console.ReadLine()
        If DecayConstant < 0 Then
            Console.WriteLine("Error: ArgumentOutOfRangeException")
            DecayConstant = 0.5
            Console.WriteLine("Decay Constant Has Been Set To The Biggest Integer")
        ElseIf DecayConstant > 1 Then
            Console.WriteLine("Output Is Insignificant")
        End If
        Console.WriteLine()

        Console.WriteLine("Please Enter the Revive Parameter")
        Dim ReviveParameter As Double = Console.ReadLine()
        If ReviveParameter > 1 Then
            Console.WriteLine("Error: ArgumentOutOfRangeException")
            ReviveParameter = 1
            Console.WriteLine("Dimension Has Been Set To The Biggest Integer")
            Console.WriteLine("No Revive Effect")
        ElseIf ReviveParameter < 0.9 Then
            Console.WriteLine("Output Is Insignificant")
        End If

        Console.WriteLine("Press Enter To Continue")
        Console.ReadKey()

        Dim MainArray(Dimension, Dimension) As Nuclei

        For i As Integer = 0 To Dimension
            For j As Integer = 0 To Dimension

                MainArray(i, j) = New Nuclei
                MainArray(i, j).SetDecayConstant(DecayConstant)

            Next j
        Next i

        Do

            Console.Clear()

            For i As Integer = 0 To Dimension
                For j As Integer = 0 To Dimension

                    MainArray(i, j).Decay()

                    VBMath.Randomize()
                    Dim RandomNumber As Double = VBMath.Rnd()
                    If RandomNumber > ReviveParameter Then
                        If i <> 0 And j <> 0 Then
                            MainArray(i - 1, j - 1).ChangeIfAffected()
                        End If
                        If i <> 0 Then
                            MainArray(i - 1, j).ChangeIfAffected()
                        End If
                        If i <> 0 And j <> Dimension Then
                            MainArray(i - 1, j + 1).ChangeIfAffected()
                        End If
                        If j <> 0 Then
                            MainArray(i, j - 1).ChangeIfAffected()
                        End If
                        If i <> Dimension And j <> 0 Then
                            MainArray(i + 1, j - 1).ChangeIfAffected()
                        End If
                        If i <> Dimension Then
                            MainArray(i + 1, j).ChangeIfAffected()
                        End If
                        If j <> Dimension Then
                            MainArray(i, j + 1).ChangeIfAffected()
                        End If
                        If i <> Dimension And j <> Dimension Then
                            MainArray(i + 1, j + 1).ChangeIfAffected()
                        End If
                    End If

                    If MainArray(i, j).IfAffected = True Then
                        MainArray(i, j).IfDecayed = False
                    End If

                    MainArray(i, j).Draw()
                    MainArray(i, j).IfAffected = False

                Next j
                Console.WriteLine()
            Next i

            Console.ReadKey()

        Loop

    End Sub

    Public Sub Initialization()

        Console.BackgroundColor = ConsoleColor.Black
        Console.Title = "Nuclear Decay Simulator"
        Console.ForegroundColor = ConsoleColor.Green
        Console.WindowHeight = 56

    End Sub

    Public Class Nuclei

        Public IfDecayed As Boolean = False
        Private DecayConstant As Double = Math.E
        Public IfAffected As Boolean = False

        Public Sub SetIfDecayed(Input As Boolean)
            IfDecayed = Input
        End Sub

        Public Sub SetDecayConstant(Input As Double)
            DecayConstant = Input
        End Sub

        Public Sub ChangeIfAffected()
            IfAffected = True
        End Sub

        Public Sub Decay()

            VBMath.Randomize()

            Dim p As Double = 1 - Math.E ^ (-DecayConstant)
            Dim RandomNumber As Double = VBMath.Rnd()

            If RandomNumber < p Then
                SetIfDecayed(True)
            End If

        End Sub

        Public Sub Draw()

            If IfDecayed = False Then
                Console.Write("■")
            Else
                Console.Write("□")
            End If

        End Sub

    End Class

End Module
