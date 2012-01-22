namespace Big_McGreed.content.input
{
    /// <summary>
    /// Types of hook that can be installed using the SetWindwsHookEx function.
    /// </summary>
    public enum HookType
    {
        WH_CALLWNDPROC = 4,
        WH_CALLWNDPROCRET = 12,
        WH_CBT = 5,
        WH_DEBUG = 9,
        WH_FOREGROUNDIDLE = 11,
        WH_GETMESSAGE = 3,
        WH_HARDWARE = 8,
        WH_JOURNALPLAYBACK = 1,
        WH_JOURNALRECORD = 0,
        WH_KEYBOARD = 2,
        WH_KEYBOARD_LL = 13,
        WH_MAX = 11,
        WH_MAXHOOK = WH_MAX,
        WH_MIN = -1,
        WH_MINHOOK = WH_MIN,
        WH_MOUSE_LL = 14,
        WH_MSGFILTER = -1,
        WH_SHELL = 10,
        WH_SYSMSGFILTER = 6,
    };
}
