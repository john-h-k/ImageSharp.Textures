﻿using System;
using ImGuiNET;

namespace Phoenix.Import.Application.UI.WizardPages
{
    public class Welcome : WizardPage
    {
        public Welcome(Wizard wizard) : base(wizard)
        {
        }

        public override void Initialize()
        {
        }

        public override void Render()
        {
            this.Wizard.PreviousButton.Visble = false;
            this.Wizard.CancelButton.Visble = false;

            ImGui.TextWrapped("Welcome");
            ImGui.Spacing();
            ImGui.Spacing();
            ImGui.TextWrapped("Welcome to the ImageSharp Textures Test tool.");
        }
    }
}
