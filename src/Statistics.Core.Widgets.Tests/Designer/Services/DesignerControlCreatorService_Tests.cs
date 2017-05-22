using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Statistics.Core.Widgets.Designer
{
    [TestClass()]
    public class DesignerControlCreatorService_Tests
    {

        private IDesignerControlCreatorService CreateService()
        {
            return new DesignerControlCreatorService();
        }


        #region Text constants
        private const int controlPosition = 572;
        private const string originalLayout = @"
<UserControl x:Class=""ReportLoader.VisualizerControl""
             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
             xmlns:local=""clr-namespace:ReportLoader""
             mc:Ignorable=""d""
             d: DesignHeight=""300"" d: DesignWidth =""300"">
<UserControl.Resources>
</UserControl.Resources>
    <Grid>
    </Grid>
</UserControl>";
        private const string cotrnolStyle = @"        <Style x:Key=""TestStyle"" TargetType=""{x:Type TextBlock}"">
            <Setter Property = ""IsEnabled"" Value=""True""/>
        </Style>";
        private const string controlDefinition = @"<TextBlock Style=""{StaticResource TestStyle}"" Text=""test textblock text!"" />";

        private const string expectedLayout = @"
<UserControl x:Class=""ReportLoader.VisualizerControl""
             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
             xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
             xmlns:local=""clr-namespace:ReportLoader""
             mc:Ignorable=""d""
             d: DesignHeight=""300"" d: DesignWidth =""300"">
<UserControl.Resources>        <Style x:Key=""TestStyle"" TargetType=""{x:Type TextBlock}"">
            <Setter Property = ""IsEnabled"" Value=""True""/>
        </Style>
</UserControl.Resources>
    <Grid><TextBlock Style=""{StaticResource TestStyle}"" Text=""test textblock text!"" />
    </Grid>
</UserControl>";
        
        #endregion



        [TestMethod()]
        public void ShouldInsertControlSnippet()
        {
            //arrange
            var controlCreator = new Mock<ControlCreator>();

            controlCreator
                .Setup(d => d.Create())
                .Returns(() => new DesignerControlDefinition { Control = controlDefinition, Style = cotrnolStyle });
            var service = CreateService();


            //act
            var result = service.InsertControlSnippet(controlCreator.Object, originalLayout, controlPosition);


            //assert
            Assert.AreEqual(expectedLayout, result);
        }
    }
}