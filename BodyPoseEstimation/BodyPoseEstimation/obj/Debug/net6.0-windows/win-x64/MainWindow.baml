   M S B A M L   `   `   `  ����M  IBodyPoseEstimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BodyPoseEstimationBodyPoseEstimation  5   	   U/clr-namespace:BodyPoseEstimation.MVVM.ViewModel!BodyPoseEstimation.MVVM.ViewModel  5   	   K*clr-namespace:BodyPoseEstimation.MVVM.ViewBodyPoseEstimation.MVVM.View  5   	   #    BodyPoseEstimation.MainWindow   5   	   W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:BodyPoseEstimation     5   	   E	viewModel/clr-namespace:BodyPoseEstimation.MVVM.ViewModel     5   	   ;view*clr-namespace:BodyPoseEstimation.MVVM.View     5   	   -   5        $	��600��5
   	   $	��920��6     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      5  /BodyPoseEstimation.MVVM.ViewModel.MainViewModel  5   
   6$   .��5      �� ���#272537 �� CornerRadius$ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      �� 5      $
��auto �6#    � RowDefinitions 5      �� 5      $��75 �6    �� 5      .+�5!      �� $
��CWRU��6   $��Center=�5"      $
��Left=�5#      
�������$��22(�5%      $��50 0 0 0q�5&      $��/Fonts/#Poppins)�5'      �� 5*      $��1��6   .+�5+      	� $
��Home��6   $��50��5,      
�������$��14(�5.         MenuButtonTheme#��[  5/       d� 	IsChecked$
 TrueY�50      ��51      �� *HomeViewCommand+	� 53      $��About��6   $��50��54      
�������$��14(�56      #��[  57      ��58      �� *AboutViewCommand+59      �� 5:      $��Body Pose Estimation��6   $��Center=�5;      $
��Left=�5<      
�������$��22(�5>      $��5q�5?      $��1��5@      $��/Fonts/#Poppins)�5A      �� 5D      $��2��6   
���\\��$��30��5F      $��30��5G      $��20q�5H      $	��Top=�5I      ��5J      �� *CloseCommand+5L      �� 5N      $��1��6   $��1��5O      $��2��5P      $��10q�5Q      ��5R      �� *CurrentView+5S      5T      5U      