 **Agregar paquete de Nuget**
1- Agrega el siguiente paquete Nuget **Xamarin.Forms.BehaviorsPack version, la que se observa en la imagen

![behaviorsPack](https://user-images.githubusercontent.com/45072377/144543286-1b852822-81d4-42f7-a5a1-ad0f7bd55f04.png)

2. En el archivo **LaboratoryListPage.xaml** agrega el siguiente marcado xaml.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:behaviors="clr-namespace:Xamarin.Forms.BehaviorsPack;assembly=Xamarin.Forms.BehaviorsPack"
             x:Class="Attendance.Views.LaboratoriesPage">
    <ContentPage.Content>
        <StackLayout>
            <Label Text="LISTA DE LABORATORIOS:"
                   FontSize="Large"
                   FontAttributes="Bold"/>
<ListView ItemsSource="{Binding Laboratories}"
                      SelectedItem="{Binding SelectedLaboratory}">
                <ListView.Behaviors>
                    <behaviors:EventToCommandBehavior 
                        EventName="ItemSelected" 
                        Command="{Binding ShowStudentsOfLaboratorySelectedCommand}"/>
                </ListView.Behaviors>
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding LabName}"
                                  Detail="{Binding Id}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
            <Label Text="Students:"
                   FontSize="Large"
                   FontAttributes="Bold"/>
            <ListView ItemsSource="{Binding Students}">
<ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding StudentName}"
                                  Detail="{Binding Id}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
Vamos a crear a continuaci??n la vista para crear un nuevo laboratorio.
