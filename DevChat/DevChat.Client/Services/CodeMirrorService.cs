using DevChat.Client.Components;
using Microsoft.JSInterop;

namespace DevChat.Client.Services;

public class CodeMirrorService
{
    private delegate void SendMessage();
    private delegate void OnKeyDown();

    private static SendMessage m_SendMessage = null;
    private static OnKeyDown m_OnKeyDown = null;

    public static void Register(Conversation conversation)
    {
        m_SendMessage += conversation.OnSendKeyEvent;
        m_OnKeyDown += conversation.OnKeyDown;
    }

    public static void Unregister(Conversation conversation)
    {
        if (m_SendMessage != null)
        {
            m_SendMessage -= conversation.OnSendKeyEvent;
            m_OnKeyDown -= conversation.OnKeyDown;
        }
    }

    [JSInvokable]
    public static void OnSendMessage()
    {
        if (m_SendMessage != null)
        {
            m_SendMessage();
        }
    }

    [JSInvokable]
    public static void IndicateTyping()
    {
        if (m_OnKeyDown != null)
        {
            m_OnKeyDown();
        }
    }
}
