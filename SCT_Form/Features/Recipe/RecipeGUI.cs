using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    // 이 파일은 필드/생성자만 담당한다. 나머지 책임은 분리되어 있다:
    // - RecipeGUI.Editor.cs: PM 선택 버튼 + PM별 입력 필드 폼 구성
    // - RecipeGUI.FileOps.cs: Recipe 파일 목록/저장/삭제/PPID 중복 체크
    // - RecipeGUI.ProcessRecipeSwitch.cs: Chamber/Process Recipe 화면 전환, 폴더 선택 다이얼로그
    public partial class RecipeGUI : UserControl
    {
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Dictionary<string, Control> recipeInputs = new Dictionary<string, Control>();
        private MainGUI main;
        private ProcessRecipe processRecipeGUI;
        private string currentPm = "PM A";
        private string currentRecipePath;
        private bool isProcessRecipeMode;

        public RecipeGUI()
        {
            InitializeComponent();
            InitializeRecipeScreen();
        }

        public RecipeGUI(MainGUI mainGUI)
        {
            InitializeComponent();
            main = mainGUI;
            InitializeRecipeScreen();
        }

        internal void ShowDefaultPmA()
        {
            SelectPm("PM A");
        }

        private void InitializeRecipeScreen()
        {
            listView_Recipe.View = View.List;
            listView_Recipe.FullRowSelect = true;
            listView_Recipe.MultiSelect = false;
            listView_Recipe.Font = new Font(listView_Recipe.Font.FontFamily, 22F, FontStyle.Regular);
            listView_Recipe.SelectedIndexChanged += listView_Recipe_SelectedIndexChanged;

            btn_RecipeNew.Click += btn_RecipeNew_Click;
            btn_RecipeSave.Click += btn_RecipeSave_Click;
            btn_RecipeSaveAs.Click += btn_RecipeSaveAs_Click;
            btn_RecipeDelete.Click += btn_RecipeDelete_Click;
            btn_OpenFolder.Click += btn_OpenFolder_Click;

            SelectPm("PM A");
        }
    }
}
