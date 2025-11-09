using System;

namespace Assets.src.g
{
    public class RegisterScreen : mScreen, IActionListener
    {
        public TField tfUser;

        public TField tfPass;

        public static bool isContinueToLogin = false;

        private int focus;

        private int wC;

        private int yL;

        private int defYL;

        public bool isCheck;

        public bool isRes;

        private Command cmdLogin;

        private Command cmdCheck;

        private Command cmdGoBack;

        private Command cmdRes;

        private Command cmdMenu;

        private Command cmdBackFromRegister;

        public string listFAQ = string.Empty;

        public string titleFAQ;

        public string subtitleFAQ;

        private string numSupport = string.Empty;

        private string strUser;

        private string strPass;

        public static bool isLocal = false;

        public static bool isUpdateAll;

        public static bool isUpdateData;

        public static bool isUpdateMap;

        public static bool isUpdateSkill;

        public static bool isUpdateItem;

        public static string serverName;

        public static Image imgTitle;

        public int plX;

        public int plY;

        public int lY;

        public int lX;

        public int logoDes;

        public int lineX;

        public int lineY;

        public static int[] bgId = new int[5] { 0, 8, 2, 6, 9 };

        public static bool isTryGetIPFromWap;

        public static short timeLogin;

        public static long lastTimeLogin;

        public static long currTimeLogin;

        private int yt;

        private Command cmdSelect;

        private Command cmdRegister;

        private int xLog;

        private int yLog;

        private int xP;

        private int yP;

        private int wP;

        private int hP;

        private string passRe = string.Empty;

        public bool isFAQ;

        private int tipid = -1;

        public bool isLogin2;

        private int v = 2;

        private int g;

        private int ylogo = -40;

        private int dir = 1;

        public static bool isLoggingIn;

        public RegisterScreen(sbyte haveName)
        {
            yLog = GameCanvas.hh - 30;
            TileMap.bgID = (sbyte)(mSystem.currentTimeMillis() % 9);
            if (TileMap.bgID == 5 || TileMap.bgID == 6)
            {
                TileMap.bgID = 4;
            }
            GameScr.loadCamera(fullmScreen: true, -1, -1);
            GameScr.cmx = 100;
            GameScr.cmy = 200;
            if (GameCanvas.h > 200)
            {
                defYL = GameCanvas.hh - 80;
            }
            else
            {
                defYL = GameCanvas.hh - 65;
            }
            resetLogo();
            wC = ((GameCanvas.w < 200) ? 140 : 160);
            yt = GameCanvas.hh - mScreen.ITEM_HEIGHT - 5;
            if (GameCanvas.h <= 160)
            {
                yt = 20;
            }
            tfUser = new TField();
            tfUser.y = GameCanvas.hh - mScreen.ITEM_HEIGHT - 9;
            tfUser.width = wC;
            tfUser.height = mScreen.ITEM_HEIGHT + 2;
            tfUser.isFocus = true;
            tfUser.setInputType(TField.INPUT_TYPE_ANY);
            tfUser.name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email;
            if (haveName == 1)
            {
                tfUser.setText("123456");
            }
            tfPass = new TField();
            tfPass.y = GameCanvas.hh - 4;
            tfPass.width = wC;
            tfPass.height = mScreen.ITEM_HEIGHT + 2;
            tfPass.setInputType(TField.INPUT_TYPE_PASSWORD);
            tfPass.name = mResources.password;
            if (haveName == 1)
            {
                tfPass.setText("123456");
            }
            yt += 35;
            isCheck = true;
            focus = 0;
            cmdLogin = new Command((GameCanvas.w <= 200) ? mResources.login2 : mResources.login, GameCanvas.instance, 888393, null);
            cmdCheck = new Command(mResources.remember, this, 2001, null);
            cmdRes = new Command(mResources.register, this, 2002, null);
            cmdBackFromRegister = new Command(mResources.CANCEL, this, 10021, null);
            left = (cmdMenu = new Command(mResources.MENU, this, 2003, null));
            if (GameCanvas.isTouch)
            {
                cmdLogin.x = GameCanvas.w / 2 + 8;
                cmdMenu.x = GameCanvas.w / 2 - mScreen.cmdW - 8;
                if (GameCanvas.h >= 200)
                {
                    cmdLogin.y = yLog + 110;
                    cmdMenu.y = yLog + 110;
                }
                cmdBackFromRegister.x = GameCanvas.w / 2 + 3;
                cmdBackFromRegister.y = yLog + 110;
                cmdRes.x = GameCanvas.w / 2 - 84;
                cmdRes.y = cmdMenu.y;
            }
            wP = 170;
            hP = ((!isRes) ? 100 : 110);
            xP = GameCanvas.hw - wP / 2;
            yP = tfUser.y - 15;
            int num = 4;
            int num2 = num * 32 + 23 + 33;
            if (num2 >= GameCanvas.w)
            {
                num--;
                num2 = num * 32 + 23 + 33;
            }
            xLog = GameCanvas.w / 2 - num2 / 2;
            yLog = GameCanvas.hh - 30;
            lY = ((GameCanvas.w < 200) ? (tfUser.y - 30) : (yLog - 30));
            tfUser.x = xLog + 10;
            tfUser.y = yLog + 20;
            cmdRegister = new Command(mResources.register, this, 2008, null);
            cmdRegister.x = GameCanvas.w / 2 - 84;
            cmdRegister.y = cmdLogin.y;
            cmdGoBack = new Command("Thoát", this, 1003, null);
            cmdGoBack.x = GameCanvas.w / 2 + 3;
            cmdGoBack.y = cmdLogin.y;
            if (GameCanvas.w < 250)
            {
                cmdRegister.x = GameCanvas.w / 2 - 80;
                cmdGoBack.x = GameCanvas.w / 2 + 10;
                cmdGoBack.y = (cmdRegister.y = GameCanvas.h - 25);
            }
            center = cmdRegister;
            left = cmdGoBack;
        }

        public void resetLogo()
        {
            yL = -50;
        }

        public new void switchToMe()
        {
            Res.outz("Res switch");
            SoundMn.gI().stopAll();
            focus = 0;
            tfUser.isFocus = true;
            tfPass.isFocus = false;
            if (GameCanvas.isTouch)
            {
                tfUser.isFocus = false;
                focus = -1;
            }
            base.switchToMe();
        }

        protected int loadIndexServer()
        {
            return Rms.loadRMSInt("indServer");
        }

        public override void update()
        {
            tfUser.update();
            tfPass.update();
            for (int i = 0; i < Effect2.vEffect2.size(); i++)
            {
                ((Effect2)Effect2.vEffect2.elementAt(i)).update();
            }
            if (isUpdateAll && !isUpdateData && !isUpdateItem && !isUpdateMap && !isUpdateSkill)
            {
                isUpdateAll = false;
                mSystem.gcc();
                Service.gI().finishUpdate();
            }
            GameScr.cmx++;
            if (GameScr.cmx > GameCanvas.w * 3 + 100)
            {
                GameScr.cmx = 100;
            }
            if (ChatPopup.currChatPopup != null)
            {
                return;
            }
            GameCanvas.debug("LGU1", 0);
            GameCanvas.debug("LGU2", 0);
            GameCanvas.debug("LGU3", 0);
            updateLogo();
            GameCanvas.debug("LGU4", 0);
            GameCanvas.debug("LGU5", 0);
            if (g >= 0)
            {
                ylogo += dir * g;
                g += dir * v;
                if (g <= 0)
                {
                    dir *= -1;
                }
                if (ylogo > 0)
                {
                    dir *= -1;
                    g -= 2 * v;
                }
            }
            GameCanvas.debug("LGU6", 0);
            if (tipid >= 0 && GameCanvas.gameTick % 100 == 0)
            {
                doChangeTip();
            }
            if (GameCanvas.isTouch)
            {
                if (isRes)
                {
                    center = cmdRes;
                    left = cmdBackFromRegister;
                }
                else
                {
                    center = cmdRegister;
                    left = cmdGoBack;
                }
            }
            else if (isRes)
            {
                center = cmdRes;
                left = cmdBackFromRegister;
            }
            else
            {
                center = cmdRegister;
                left = cmdGoBack;
            }
        }

        public void updateLogo()
        {
            if (defYL != yL)
            {
                yL += defYL - yL >> 1;
            }
        }
        private void doChangeTip()
        {
            tipid++;
            if (tipid >= mResources.tips.Length)
            {
                tipid = 0;
            }
            if (GameCanvas.currentDialog == GameCanvas.msgdlg && GameCanvas.msgdlg.isWait)
            {
                GameCanvas.msgdlg.setInfo(mResources.tips[tipid]);
            }
        }

        public override void keyPress(int keyCode)
        {
            if (tfUser.isFocus)
            {
                tfUser.keyPressed(keyCode);
            }
            else if (tfPass.isFocus)
            {
                tfPass.keyPressed(keyCode);
            }
            base.keyPress(keyCode);
        }

        public override void unLoad()
        {
            base.unLoad();
        }

        public override void paint(mGraphics g)
        {
            GameCanvas.debug("PLG1", 1);
            GameCanvas.paintBGGameScr(g);
            GameCanvas.debug("PLG2", 2);
            int num = tfUser.y - 70;
            if (GameCanvas.h <= 220)
            {
                num += 5;
            }
            mFont.tahoma_7_white.drawString(g, "v" + GameMidlet.VERSION + "(" + mGraphics.zoomLevel + ")", GameCanvas.w - 2, 17, 1, mFont.tahoma_7_grey);
            mFont.tahoma_7b_white.drawString(g, "Dưới 18 tuổi chỉ có thể chơi", 5, 2, 0, mFont.tahoma_7b_dark);
            mFont.tahoma_7b_white.drawString(g, "180p 1 ngày", 5, 17, 0, mFont.tahoma_7b_dark);
            if (mSystem.clientType == 1 && !GameCanvas.isTouch)
            {
                mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, GameCanvas.h - 15, 1, mFont.tahoma_7_grey);
            }
            else
            {
                mFont.tahoma_7_white.drawString(g, ServerListScreen.linkweb, GameCanvas.w - 2, 2, 1, mFont.tahoma_7_grey);
            }
            if (ChatPopup.currChatPopup != null || ChatPopup.serverChatPopUp != null)
            {
                return;
            }
            if (GameCanvas.currentDialog == null)
            {
                int h = 105;
                int w = ((GameCanvas.w < 200) ? 160 : 180);
                PopUp.paintPopUp(g, xLog, yLog - 10, w, h, -1, isButton: true);
                if (GameCanvas.h > 160 && imgTitle != null)
                {
                    g.drawImage(imgTitle, GameCanvas.hw, num, 3);
                }
                GameCanvas.debug("PLG4", 1);
                int num2 = 4;
                int num3 = num2 * 32 + 23 + 33;
                if (num3 >= GameCanvas.w)
                {
                    num2--;
                    num3 = num2 * 32 + 23 + 33;
                }
                xLog = GameCanvas.w / 2 - num3 / 2;
                tfUser.x = xLog + 10;
                tfUser.y = yLog + 20;
                tfPass.x = xLog + 10;
                tfPass.y = yLog + 55;
                tfUser.paint(g);
                tfPass.paint(g);
                if (GameCanvas.w < 176)
                {
                    mFont.tahoma_7b_green2.drawString(g, mResources.acc + ":", tfUser.x - 35, tfUser.y + 7, 0);
                    mFont.tahoma_7b_green2.drawString(g, mResources.pwd + ":", tfPass.x - 35, tfPass.y + 7, 0);
                    mFont.tahoma_7b_green2.drawString(g, mResources.server + ":" + serverName, GameCanvas.w / 2, tfPass.y + 32, 2);
                }
            }
            base.paint(g);
        }

        private void turnOffFocus()
        {
            tfUser.isFocus = false;
            tfPass.isFocus = false;
        }

        private void processFocus()
        {
            turnOffFocus();
            switch (focus)
            {
                case 0:
                    tfUser.isFocus = true;
                    break;
                case 1:
                    tfPass.isFocus = true;
                    break;
            }
        }

        public override void updateKey()
        {
            if (isContinueToLogin)
            {
                return;
            }
            if (!GameCanvas.isTouch)
            {
                if (tfUser.isFocus)
                {
                    right = tfUser.cmdClear;
                }
                else if (tfPass.isFocus)
                {
                    right = tfPass.cmdClear;
                }
            }
            if (GameCanvas.keyPressed[21])
            {
                focus--;
                if (focus < 0)
                {
                    focus = 1;
                }
                processFocus();
            }
            else if (GameCanvas.keyPressed[22])
            {
                focus++;
                if (focus > 1)
                {
                    focus = 0;
                }
                processFocus();
            }
            if (GameCanvas.keyPressed[21] || GameCanvas.keyPressed[22])
            {
                GameCanvas.clearKeyPressed();
                if (!isLogin2 || isRes)
                {
                    if (focus == 1)
                    {
                        tfUser.isFocus = false;
                        tfPass.isFocus = true;
                    }
                    else if (focus == 0)
                    {
                        tfUser.isFocus = true;
                        tfPass.isFocus = false;
                    }
                    else
                    {
                        tfUser.isFocus = false;
                        tfPass.isFocus = false;
                    }
                }
            }
            if (GameCanvas.isTouch)
            {
                if (isRes)
                {
                    center = cmdRes;
                    left = cmdBackFromRegister;
                }
                else
                {
                    center = cmdRegister;
                    left = cmdGoBack;
                }
            }
            else if (isRes)
            {
                center = cmdRes;
                left = cmdBackFromRegister;
            }
            else
            {
                center = cmdRegister;
                left = cmdGoBack;
            }
            if (GameCanvas.isPointerJustRelease)
            {
                if (GameCanvas.isPointerHoldIn(tfUser.x, tfUser.y, tfUser.width, tfUser.height))
                {
                    focus = 0;
                    processFocus();
                }
                else if (GameCanvas.isPointerHoldIn(tfPass.x, tfPass.y, tfPass.width, tfPass.height))
                {
                    focus = 1;
                    processFocus();
                }
            }
            base.updateKey();
            GameCanvas.clearKeyPressed();
        }

        public void perform(int idAction, object p)
        {
            switch (idAction)
            {
                case 1000:
                    try
                    {
                        GameMidlet.instance.platformRequest((string)p);
                    }
                    catch (Exception ex)
                    {
                        ex.StackTrace.ToString();
                    }
                    GameCanvas.endDlg();
                    break;
                case 1001:
                    GameCanvas.endDlg();
                    isRes = false;
                    break;
                case 1004:
                    ServerListScreen.doUpdateServer();
                    GameCanvas.serverScreen.switchToMe();
                    break;
                case 10021:
                    actRegisterLeft();
                    break;
                case 1003:
                    Session_ME.gI().close();
                    GameCanvas.serverScreen.switchToMe();
                    break;
                case 1005:
                    try
                    {
                        GameMidlet.instance.platformRequest("http://dauphainro.com");
                        break;
                    }
                    catch (Exception ex2)
                    {
                        ex2.StackTrace.ToString();
                        break;
                    }
                case 2001:
                    if (isCheck)
                    {
                        isCheck = false;
                    }
                    else
                    {
                        isCheck = true;
                    }
                    break;
                case 2002:
                    doRegister();
                    break;
                case 2003:
                    doMenu();
                    break;
                case 2004:
                    actRegister();
                    break;
                case 2008:
                    if (tfUser.getText().Equals(string.Empty) || tfPass.getText().Equals(string.Empty))
                    {
                        GameCanvas.startOKDlg("Vui lòng điền đầy đủ thông tin");
                        break;
                    }
                    GameCanvas.startOKDlg(mResources.PLEASEWAIT);
                    Service.gI().charInfo(tfUser.getText(), tfPass.getText());
                    break;
                case 4000:
                    doRegister(tfUser.getText());
                    break;
            }
        }
        public void actRegisterLeft()
        {
            if (isLogin2)
            {
                doLogin();
                return;
            }
            isRes = false;
            tfUser.isFocus = true;
            tfPass.isFocus = false;
            left = cmdMenu;
        }

        public void doLogin()
        {
        }

        public void actRegister()
        {
            GameCanvas.endDlg();
            GameCanvas.startOKDlg(mResources.regNote);
            isRes = true;
            tfUser.isFocus = true;
            tfPass.isFocus = false;
        }

        public void backToRegister()
        {
            if (GameCanvas.loginScr.isLogin2)
            {
                GameCanvas.startYesNoDlg(mResources.note, new Command(mResources.YES, GameCanvas.panel, 10019, null), new Command(mResources.NO, GameCanvas.panel, 10020, null));
                return;
            }
            GameCanvas.instance.doResetToLoginScr(GameCanvas.loginScr);
            Session_ME.gI().close();
        }

        protected void doRegister(string user)
        {
        }

        public void savePass()
        {
        }
        protected void doMenu()
        {
            MyVector myVector = new MyVector("vMenu Login");
            myVector.addElement(new Command(mResources.registerNewAcc, this, 2004, null));
            if (!isLogin2)
            {
                myVector.addElement(new Command(mResources.selectServer, this, 1004, null));
            }
            myVector.addElement(new Command(mResources.forgetPass, this, 1003, null));
            myVector.addElement(new Command(mResources.website, this, 1005, null));
            if (Rms.loadRMSInt("lowGraphic") == 1)
            {
                myVector.addElement(new Command(mResources.increase_vga, this, 10041, null));
            }
            else
            {
                myVector.addElement(new Command(mResources.decrease_vga, this, 10042, null));
            }
            myVector.addElement(new Command(mResources.EXIT, GameCanvas.instance, 8885, null));
            GameCanvas.menu.startAt(myVector, 0);
        }
        protected void doRegister()
        {
            if (tfUser.getText().Equals(string.Empty))
            {
                GameCanvas.startOKDlg(mResources.userBlank);
                return;
            }
            int num = 0;
            string text = null;
            if (mResources.language == 2)
            {
                if (tfUser.getText().IndexOf("@") == -1 || tfUser.getText().IndexOf(".") == -1)
                {
                    text = mResources.emailInvalid;
                }
                num = 0;
            }
            else
            {
                try
                {
                    long.Parse(tfUser.getText());
                    if (tfUser.getText().Length < 8 || tfUser.getText().Length > 12 || (!tfUser.getText().StartsWith("0") && !tfUser.getText().StartsWith("84")))
                    {
                        text = mResources.phoneInvalid;
                    }
                    num = 1;
                }
                catch (Exception)
                {
                    if (tfUser.getText().IndexOf("@") == -1 || tfUser.getText().IndexOf(".") == -1)
                    {
                        text = mResources.emailInvalid;
                    }
                    num = 0;
                }
            }
            if (text != null)
            {
                GameCanvas.startOKDlg(text);
            }
            else
            {
                GameCanvas.msgdlg.setInfo(mResources.plsCheckAcc + ((num != 1) ? (mResources.email + ": ") : (mResources.phone + ": ")) + tfUser.getText() + "\n" + mResources.password + ": " + tfPass.getText(), new Command(mResources.ACCEPT, this, 4000, null), null, new Command(mResources.NO, GameCanvas.instance, 8882, null));
            }
            GameCanvas.currentDialog = GameCanvas.msgdlg;
        }
    }
}
