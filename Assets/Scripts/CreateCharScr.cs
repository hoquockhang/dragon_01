using System;
using UnityEngine;

public class CreateCharScr : mScreen, IActionListener
{
    public static CreateCharScr instance;

    private PopUp p;

    public static bool isCreateChar = false;

    public static TField tAddName;

    public static int indexGender;

    public static int indexHair;

    public static int selected;

    public static int[][] hairID = new int[3][]
    {
        new int[3] { 64, 30, 31 },
        new int[3] { 9, 29, 32 },
        new int[3] { 6, 27, 28 }
    };

    public static int[] defaultLeg = new int[3] { 2, 13, 8 };

    public static int[] defaultBody = new int[3] { 1, 12, 7 };

    private int yButton;

    private int disY;

    private int[] bgID = new int[3] { 0, 4, 8 };

    public int yBegin;

    private int curIndex;

    private int cx = 168;

    private int cy = 350;

    private int dy = 45;

    private int cp1;

    private int cf;

    private Texture2D background;
    private Image imgSoil;
    private CmdLine[] cmdGender = null;
    private Image[] aura = new Image[3];
    private Image[] mount = new Image[3];
    private CmdLine cmdOK, cmdBack;
    public CreateCharScr()
    {
        indexGender = Res.random(0, 2);
        cmdGender = new CmdLine[]
        {
            new CmdLine("Trái Đất", this, 1),
            new CmdLine("Namek", this, 2),
            new CmdLine("Xayda", this, 3)
        };
        tAddName = new TField();
        tAddName.width = 160;
        tAddName.height = mScreen.ITEM_HEIGHT + 2;
        tAddName.setIputType(TField.INPUT_TYPE_ANY);
        tAddName.x = GameCanvas.w / 2 - tAddName.width / 2;
        tAddName.y = 20;
        tAddName.name = "Tên Nhân Vật";
        if (!GameCanvas.isTouch)
        {
            tAddName.isFocus = true;
        }
        cmdOK = new CmdLine("OK", this, 8000);
        cmdBack = new CmdLine("Trở Lại", this, 8001);
    }
    #region LinhTinh
    public static CreateCharScr gI()
    {
        if (instance == null)
        {
            instance = new CreateCharScr();
        }
        return instance;
    }

    public override void switchToMe()
    {
        LoginScr.isContinueToLogin = false;
        GameCanvas.menu.showMenu = false;
        GameCanvas.endDlg();
        base.switchToMe();
        indexGender = Res.random(0, 2);
        indexHair = Res.random(0, 3);
        Char.isLoadingMap = false;
    }
    public override void keyPress(int keyCode)
    {
        if (tAddName != null && tAddName.isFocus)
            tAddName.keyPressed(keyCode);
        base.keyPress(keyCode);
    }

    public override void update()
    {
        cp1++;
        if (cp1 > 30)
        {
            cp1 = 0;
        }
        if (cp1 % 15 < 5)
        {
            cf = 0;
        }
        else
        {
            cf = 1;
        }
        if (tAddName != null)
        {
            tAddName.update();
        }
    }

    public override void updateKey()
    {
        for (int i = 0; i < cmdGender.Length; i++)
        {
            if (cmdGender[i] != null && cmdGender[i].isPointerPressInside())
            {
                cmdGender[i].performAction();
            }
        }
        if (cmdBack != null && cmdBack.isPointerPressInside())
        {
            cmdBack.performAction();
        }
        if (cmdOK != null && cmdOK.isPointerPressInside())
        {
            cmdOK.performAction();
        }
        if (tAddName != null)
        {
            if (GameCanvas.isPointerHoldIn(tAddName.x, tAddName.y, tAddName.width, tAddName.height))
            {
                if (GameCanvas.isPointerJustRelease && GameCanvas.isPointerClick)
                {
                    tAddName.isFocus = true;
                    GameCanvas.clearAllPointerEvent();
                    return;
                }
            }
        }
        if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21]) //di len
        {
            indexGender--;
            if (indexGender < 0)
            {
                indexGender = 2;
            }
        }
        if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22]) // di xuong
        {
            indexGender++;
            if (indexGender > 2)
            {
                indexGender = 0;
            }
        }
        if (!TouchScreenKeyboard.visible)
        {
            base.updateKey();
        }
        GameCanvas.clearKeyHold();
        GameCanvas.clearKeyPressed();
    }
    #endregion
    static Image soil, btn1, btn2, img1, img2;
    static int imgmove = 0;
    public override void paint(mGraphics g)
    {
        if (Char.isLoadingMap)
        {
            return;
        }

        int num2 = hairID[indexGender][indexHair];
        int num3 = defaultLeg[indexGender];
        int num4 = defaultBody[indexGender];
        g.reset();
        if (background == null) background = Resources.Load("background2") as Texture2D;
        else
            GUI.DrawTexture(new Rect(0, -1, ScaleGUI.WIDTH + 10, ScaleGUI.HEIGHT + 2), background);
        //if (imgSoil == null)
        //{
        //    imgSoil = GameCanvas.loadImageX4("/bag/img_soil.png");
        //}
        //else
        //{
        //    g.drawImage(imgSoil, GameCanvas.w / 2, GameCanvas.h / 2 + imgSoil.getHeight() / 2, mGraphics.VCENTER | mGraphics.HCENTER);
        //}
        for (int i = 0; i < aura.Length; i++)
        {
            if (aura[i] == null)
            {
                aura[i] = GameCanvas.loadImageX4($"/bag/aura{i}.png");
            }
        }
        for (int i = 0; i < mount.Length; i++)
        {
            if (mount[i] == null)
            {
                mount[i] = GameCanvas.loadImageX4($"/bag/mount{i}.png");
            }
        }
        if (img2 == null)
        {
            img2 = GameCanvas.loadImageX4("/bag/imgMay1.png");
            return;
        }
        g.drawImage(img2, GameCanvas.w - imgmove, GameCanvas.h - 270);
        g.drawImage(img2, GameCanvas.w - imgmove, GameCanvas.h - 120);
        imgmove++;
        if (GameCanvas.w - imgmove < -img2.getWidth())
        {
            imgmove = 0;
        }
        Part part = GameScr.parts[num2];
        Part part2 = GameScr.parts[num3];
        Part part3 = GameScr.parts[num4];
        int x = GameCanvas.w / 2 - part2.pi[Char.CharInfo[cf][1][0]].dx / 2;
        int y = GameCanvas.h / 2 - part2.pi[Char.CharInfo[cf][1][0]].dy - part2.pi[Char.CharInfo[cf][1][0]].dy - part3.pi[Char.CharInfo[cf][2][0]].dy + 15;
        g.drawRegion(aura[indexGender], 0, GameCanvas.gameTick / 4 % 4 * (mGraphics.getImageHeight(aura[indexGender]) / 4), mGraphics.getImageWidth(aura[indexGender]), mGraphics.getImageHeight(aura[indexGender]) / 4, 0, x + 1 - aura[indexGender].getWidth() / 2, y + 2 - aura[indexGender].getHeight() / 4, 0);
        g.drawRegion(mount[indexGender], 0, GameCanvas.gameTick / 3 % 3 * (mGraphics.getImageHeight(mount[indexGender]) / 3), mGraphics.getImageWidth(mount[indexGender]), mGraphics.getImageHeight(mount[indexGender]) / 3, 0, x - mount[indexGender].getWidth() / 2, y + 2 - mount[indexGender].getHeight() / 6, 0);
        g.drawImage(TileMap.bong, x - TileMap.bong.getWidth() / 2, y - TileMap.bong.getHeight() / 2);
        SmallImage.drawSmallImage(g, part.pi[Char.CharInfo[cf][0][0]].id,
                x + Char.CharInfo[cf][0][1] + part.pi[Char.CharInfo[cf][0][0]].dx,
                y - Char.CharInfo[cf][0][2] + part.pi[Char.CharInfo[cf][0][0]].dy, 0, 0);
        SmallImage.drawSmallImage(g, part2.pi[Char.CharInfo[cf][1][0]].id,
             x + Char.CharInfo[cf][1][1] + part2.pi[Char.CharInfo[cf][1][0]].dx,
              y - Char.CharInfo[cf][1][2] + part2.pi[Char.CharInfo[cf][1][0]].dy, 0, 0);
        SmallImage.drawSmallImage(g, part3.pi[Char.CharInfo[cf][2][0]].id,
             x + Char.CharInfo[cf][2][1] + part3.pi[Char.CharInfo[cf][2][0]].dx,
              y - Char.CharInfo[cf][2][2] + part3.pi[Char.CharInfo[cf][2][0]].dy, 0, 0);
        mFont.tahoma_7b_white.drawString(g, "Tộc:" + " " + getGenderName(), 5, 5, 0, mFont.tahoma_7b_dark);
        for (int i = 0; i < cmdGender.Length; i++)
        {
            if (cmdGender[i] != null)
            {
                cmdGender[i].x = 20;
                cmdGender[i].y = GameCanvas.hh / 2 + (i * (CmdLine.btn0right.getHeight() + 10));
                cmdGender[i].Paint(g);
            }
        }
        if (cmdBack != null)
        {
            cmdBack.x = 5;
            cmdBack.y = GameCanvas.h - CmdLine.btn0right.getHeight();
            cmdBack.Paint(g);
        }
        if (cmdOK != null)
        {
            cmdOK.x = GameCanvas.w - 76;
            cmdOK.y = cmdBack.y;
            cmdOK.Paint(g);
        }
        if (tAddName != null)
        {
            tAddName.paint(g);
        }
        if (!TouchScreenKeyboard.visible)
        {
            base.paint(g);
        }
    }
    private string getGenderName()
    {
        switch (indexGender)
        {
            case 0: return "Trái Đất";
            case 1: return "Namek";
            case 2: return "Xayda";
            default: return string.Empty;
        }
    }

    public void perform(int idAction, object p)
    {
        switch (idAction)
        {
            case int when (idAction >= 1 && idAction <= 3):
                indexGender = idAction - 1;
                break;
            case 8000:
                if (tAddName.getText().Equals(string.Empty))
                {
                    GameCanvas.startOKDlg(mResources.char_name_blank);
                    break;
                }
                if (tAddName.getText().Length < 5)
                {
                    GameCanvas.startOKDlg(mResources.char_name_short);
                    break;
                }
                if (tAddName.getText().Length > 15)
                {
                    GameCanvas.startOKDlg(mResources.char_name_long);
                    break;
                }
                InfoDlg.showWait();
                Service.gI().createChar(tAddName.getText(), indexGender, hairID[indexGender][indexHair]);
                break;
            case 8001:
                if (GameCanvas.loginScr.isLogin2)
                {
                    GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, this, 10019, null), new Command(mResources.NO, this, 10020, null));
                    break;
                }
                if (Main.isWindowsPhone)
                {
                    GameMidlet.isBackWindowsPhone = true;
                }
                Session_ME.gI().close();
                GameCanvas.serverScreen.switchToMe();
                break;
            case 10020:
                GameCanvas.endDlg();
                break;
            case 10019:
                Session_ME.gI().close();
                GameCanvas.endDlg();
                GameCanvas.serverScreen.switchToMe();
                break;
        }
    }
}
