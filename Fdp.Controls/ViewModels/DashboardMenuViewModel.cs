using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;

namespace Fdp.Controls.ViewModels
{
    [POCOViewModel]
    public class DashboardMenuViewModel 
    {
        private DelegateCommand _LoadDataModelCommand;
        public ICommand LoadDataModelCommand
        {
            get
            {
                return _LoadDataModelCommand ?? (_LoadDataModelCommand = new DelegateCommand(() =>
                    {
                        var dialogService = this.GetService<IDialogService>();
                        UICommand result = dialogService.ShowDialog(
                            dialogCommands: new List<UICommand>() { new UICommand
                            {
                            Caption="Ok",Command=new DelegateCommand(()=>
                            {

                            }),
                            IsDefault=true,
                            Id=MessageBoxResult.OK

                            }, new UICommand {Caption="Cancel",Command=new DelegateCommand(()=> 
                            {

                            }),
                            IsDefault=true,
                            Id=MessageBoxResult.Cancel

                            } },
                            title: "Data Sources",
                            documentType: "DataModellingView",
                            parameter: "Parameter",
                            parentViewModel: this
                            );
                    }));
            }
        }

    }
}
