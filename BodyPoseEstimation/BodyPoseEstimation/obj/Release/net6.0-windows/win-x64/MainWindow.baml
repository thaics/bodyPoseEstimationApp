   M S B A M L   `   `   `  ���� M  IBodyPoseEstimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BodyPoseEstimationBodyPoseEstimation  U/clr-namespace:BodyPoseEstimation.MVVM.ViewModel!BodyPoseEstimation.MVVM.ViewModel  K*clr-namespace:BodyPoseEstimation.MVVM.ViewBodyPoseEstimation.MVVM.View  #    BodyPoseEstimation.MainWindow   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:BodyPoseEstimation     E	viewModel/clr-namespace:BodyPoseEstimation.MVVM.ViewModel     ;view*clr-namespace:BodyPoseEstimation.MVVM.View     -   $	��600��$	��920��  � WindowStyle$
  None=� � 
ResizeMode$ NoResize=�
������  � AllowsTransparency$
 True�� � WindowStartupLocation$ CenterScreen=� � DataContext 5  /BodyPoseEstimation.MVVM.ViewModel.MainViewModel  .���� ���#272537 �� CornerRadius$ 20��._��  � ColumnDefinitions �� $	��200 ��� �� $
��auto � � RowDefinitions �� $��75 ��� .+��� $
��CWRU��$��Center=�$
��Left=�
�������$��22(�$��50 0 0 0q�$��/Fonts/#Poppins)��� $��1��.+�	� $
��Home��$��50��
�������$��14(�   MenuButtonTheme#��[   d� 	IsChecked$
 TrueY����� *HomeViewCommand+	� $��About��$��50��
�������$��14(�#��[  ���� *AboutViewCommand+�� $��Body Pose Estimation��$��Center=�$
��Left=�
�������$��22(�$��5q�$��1��$��/Fonts/#Poppins)��� $��2��
���\\��$��30��$��30��$��20q�$	��Top=����� *CloseCommand+�� $��1��$��1��$��2��$��10q����� *CurrentView+