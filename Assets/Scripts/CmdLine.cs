public class CmdLine
{
    public static Image btn0left;

    public static Image btn0mid;

    public static Image btn0right;

    public static Image btn1left;

    public static Image btn1mid;

    public static Image btn1right;

    private string caption;

    private IActionListener actionListener;

    private int idAction;

    private bool focus;

    public bool isPlaySoundButton = true;

    public int x, y;
    //tạo tài khoản
    public CmdLine(string caption, IActionListener actionListener, int idAction)
    {
        this.caption = caption;
        this.actionListener = actionListener;
        this.idAction = idAction;
    }

    public void performAction()
    {
        GameCanvas.clearAllPointerEvent();
        if (isPlaySoundButton && ((caption != null && !caption.Equals(string.Empty) && !caption.Equals(mResources.saying))))
        {
            SoundMn.gI().buttonClick();
        }
        if (idAction > 0)
        {
            if (actionListener != null)
            {
                actionListener.perform(idAction, null);
            }
            else
            {
                GameScr.gI().actionPerform(idAction, null);
            }
        }
    }
    public void Paint(mGraphics g)
    {
        if (!focus)
        {
            paintOngMau(btn0left, btn0mid, btn0right, x, y, 76, g);
        }
        else
        {
            paintOngMau(btn1left, btn1mid, btn1right, x, y, 76, g);
        }
        int num = x + 38;
        if (!string.IsNullOrEmpty(caption))
        {
            if (!focus)
            {
                mFont.tahoma_7b_dark.drawString(g, caption, num, y + 7, 2);
            }
            else
            {
                mFont.tahoma_7b_green2.drawString(g, caption, num, y + 7, 2);
            }
        }
    }
    public bool isPointerPressInside()
    {
        focus = false;
        if (GameCanvas.isPointerHoldIn(x, y, 76, btn0left.getHeight()))
        {
            if (GameCanvas.isPointerDown)
            {
                focus = true;
            }
            if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
            {
                return true;
            }
        }
        return false;
    }
    public static void paintOngMau(Image img0, Image img1, Image img2, int x, int y, int size, mGraphics g)
    {
        for (int i = 10; i <= size - 20; i += 10)
        {
            g.drawImage(img1, x + i, y, 0);
        }
        int num = size % 10;
        if (num > 0)
        {
            g.drawRegion(img1, 0, 0, num, 24, 0, x + size - 10 - num, y, 0);
        }
        g.drawImage(img0, x, y, 0);
        g.drawImage(img2, x + size - 10, y, 0);
    }
}