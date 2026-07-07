using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SCT_Form
{
    // 이 파일은 필드/생성자만 담당한다. 나머지 책임은 분리되어 있다:
    // - ProcessRecipe.Editor.cs: 상세 정보 폼 + Step 순서 그리드(Process Flow) 구성
    // - ProcessRecipe.FileOps.cs: Process Recipe 파일 목록/저장/삭제/PPID 중복 체크
    public partial class ProcessRecipe : UserControl
    {
        private readonly JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        private readonly Dictionary<string, Control> detailInputs = new Dictionary<string, Control>();
        private readonly List<ProcessRecipeStep> processSteps = new List<ProcessRecipeStep>();
        private string currentRecipePath;
        private int selectedStepIndex = -1;
        private MainGUI main;

        public ProcessRecipe()
        {
            InitializeComponent();
            InitializeProcessRecipeScreen();
        }

        public ProcessRecipe(MainGUI mainGUI) : this()
        {
            main = mainGUI;
        }

        private void InitializeProcessRecipeScreen()
        {
            listView_Recipe.View = View.List;
            listView_Recipe.FullRowSelect = true;
            listView_Recipe.MultiSelect = false;
            listView_Recipe.Font = new Font(listView_Recipe.Font.FontFamily, 22F, FontStyle.Regular);
            listView_Recipe.SelectedIndexChanged += listView_Recipe_SelectedIndexChanged;

            btn_ProcessRecipeNew.Click += btn_ProcessRecipeNew_Click;
            btn_ProcessRecipeSave.Click += btn_ProcessRecipeSave_Click;
            btn_ProcessRecipeSaveAs.Click += btn_ProcessRecipeSaveAs_Click;
            btn_ProcessRecipeDelete.Click += btn_ProcessRecipeDelete_Click;
            btn_ProcessRecipeOpenFolder.Click += btn_ProcessRecipeOpenFolder_Click;

            btn_AddStep.Click += btn_AddStep_Click;
            btn_MoveUp.Click += btn_MoveUp_Click;
            btn_MoveDown.Click += btn_MoveDown_Click;
            btn_DelStep.Click += btn_DelStep_Click;

            pnl_ProcessFlow.AutoScroll = true;
            EnsureProcessRecipeFolder();
            BuildDetailEditor(null);
            RebuildProcessFlow();
            LoadProcessRecipeList();
        }
    }
}
