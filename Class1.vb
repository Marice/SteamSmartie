'
'      ===  SteamSmartie LCDSmtie Plugin by Marice Lamain (2014)  ===
'
'

Imports System.Net
Imports System.IO
Imports System.Linq
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq



' There must be a public Class that's named LCDSmartie
Public Class LCDSmartie

    'gets total gamecount
    Public Function function1(ByVal param1 As String, ByVal param2 As String) As String

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim henk As String = 0
        Dim URL As String = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=748D5EC683725E93C0BFEED083E76B90&steamid=" + param1 + "&format=json"
        Try

            request = DirectCast(WebRequest.Create(URL), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()

            Dim jResults As JObject = JObject.Parse(rawresp)
            Dim results As Generic.List(Of JToken) = jResults.Children().ToList()

            For Each item As JProperty In results
                item.CreateReader()
                henk = item.Value("game_count")

            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not response Is Nothing Then response.Close()
        End Try
        Return henk.ToString()
    End Function






    'get steam name
    Public Function function2(ByVal param1 As String, ByVal param2 As String) As String

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim henk As String = 0
        Dim URL As String = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=748D5EC683725E93C0BFEED083E76B90&steamids=" + param1
        Try

            request = DirectCast(WebRequest.Create(URL), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()

            Dim jResults As JObject = JObject.Parse(rawresp)
            Dim results As Generic.List(Of JToken) = jResults.Children().ToList()

            For Each item As JProperty In results
                item.CreateReader()

                Select Case item.Name

                    Case "response"
                        If item.Name = "response" Then
                            henk = item.Last.SelectToken("players").First.Item("personaname").ToString
                        End If
                End Select
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not response Is Nothing Then response.Close()
        End Try

        Return henk.ToString() + ""
    End Function




    'gets last played game
    Public Function function3(ByVal param1 As String, ByVal param2 As String) As String

        Dim request As HttpWebRequest
        Dim response As HttpWebResponse = Nothing
        Dim reader As StreamReader
        Dim henk As String = 0
        Dim URL As String = "http://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v0001/?key=748D5EC683725E93C0BFEED083E76B90&steamid=" + param1 + "&count=1"
        Try

            request = DirectCast(WebRequest.Create(URL), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())

            Dim rawresp As String
            rawresp = reader.ReadToEnd()

            Dim jResults As JObject = JObject.Parse(rawresp)
            Dim results As Generic.List(Of JToken) = jResults.Children().ToList()

            For Each item As JProperty In results
                item.CreateReader()

                Select Case item.Name

                    Case "response"
                        If item.Name = "response" Then
                            henk = item.Last.SelectToken("games").First.Item("name").ToString
                        End If
                End Select
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not response Is Nothing Then response.Close()
        End Try

        Return henk.ToString() + " "
    End Function






    ' You may provide/use upto 20 functions (function1 to function20).


    '
    ' Define the minimum interval that a screen should get fresh data from our plugin.
    ' The actual value used by Smartie will be the higher of this value and of the "dll check interval" setting
    ' on the Misc tab.  [This function is optional, Smartie will assume 300ms if it is not provided.]
    '
    Public Function GetMinRefreshInterval() As Integer

        Return 40000 ' 40000 ms (40 sec interval for refresh)

    End Function


End Class

