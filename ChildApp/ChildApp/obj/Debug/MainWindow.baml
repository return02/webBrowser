   M S B A M L   `   `   `  ����  ChildApp    ChildApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � WindowStyle$
 None=�5   	    � Left$	 100��5   	   
 � Top$	 100��5   	    � WindowStartupLocation$ Manual=�5   	    J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$! NavigationCommands.GoToPage��5      6`   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� -   5        $��WebUrl��5   7   $
��Auto��6   $��0��6'   6E   �� 5      $��60��6   $
��Left=�6$   $��1��6?   $!��NavigationCommands.GoToPage��6O   .��6u   To5      5      (  "System.Windows.Controls.WebBrowser  5       -   5        $��WebContentBrowser��5       $��1��62   6\   5!      5"      5#      