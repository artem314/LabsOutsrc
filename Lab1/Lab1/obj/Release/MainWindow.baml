   M S B A M L   `   `   `  ���� ?  ;Lab1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab1Lab1      Lab1.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:Lab1       � Title$  
MainWindow��$��501.33��$��722.956�� � 	Resources ��%    .��� $��10,10,10,10q� � RowDefinitions �� $	��10* ��� $	��90* �.+�� $��0�� �� �� .+�� $��2�� � ColumnDefinitions �� $
��277* ��� $
��418* �.+���  �� TextWrapping$
 Wrap=�$��0,20,0,0q����� *searchString+ �� UpdateSourceTrigger$ PropertyChanged=��� $��
Поиск��$
��Left=�$��	0,-5,0,25q�� -   $��WorkingPart��$��1�� �� $	��40* ��� $	��60* �.+�� $��1�� �� $
��357* ��� $
��125* �.+��  � DataContext �� *SelectedContact+ �� $	��14* ��� $	��14* ��� $	��14* ��� $	��17* ��� $	��14* ��� $	��14* ��� $	��14* ��� $	��44* � �� $	��25* ��� $	��75* �.+��� $��5q�$��0��$
��Wrap=�$��ФИО���� $��5q�$��1��$
��Wrap=�$-��'Стационарный телефон���� $��5q�$��2��$
��Wrap=�$'��!Мобильный телефон���� $��5q�$��3��$
��Wrap=�$��День рождения���� $��5q�$��4��$
��Wrap=�$'��!Электронный адрес���� $��5q�$��5��$
��Wrap=�$��Категория���� $��5q�$��6��$
��Wrap=�$!��Домашний адрес���� $��5q�$��7��$
��Wrap=�$��Примечание���� $��1��$��0��$��5,5,5,5q�$
 Wrap=����� *FIO+$ PropertyChanged=� �� Mode$ TwoWay=��� $��1��$��1��$��5,5,5,5q�$
 Wrap=����� *phone+$ PropertyChanged=�$ TwoWay=��� $��1��$��2��$��5,5,5,5q�$
 Wrap=����� *mobile+$ PropertyChanged=�$ TwoWay=�(  "System.Windows.Controls.DatePicker  $��1��$��3��$��5,5,5,5q�   SelectedDate �� *
birthday+$ PropertyChanged=�$ TwoWay=��� $��1��$��4��$��5,5,5,5q�$
 Wrap=����� *email+$ PropertyChanged=�$ TwoWay=��� $��1��$��6��$��5,5,5,5q�$
 Wrap=����� *	address+$ PropertyChanged=�$ TwoWay=��� $��1��$��7��$��5,5,5,5q�$
 Wrap=����� *notes+$ PropertyChanged=�$ TwoWay=�� $��1��$��5��$��0,0,5,0q� �� $	��70* ��� $	��10* ��� $	��10* ��� $	��10* �.+��� $��0��$��5,5,10,0q�	 �� 
IsEditable	 . ���� *DataContext.Categories+
 �� RelativeSource
 ��  �� AncestorType" � �� SelectedItem �� *DataContext.selectedCategory+
 �� " �$ TwoWay=� �� Text �� *DataContext.textCategory+
 �� " �$ PropertyChanged=� �� SelectedIndex �� *DataContext.indexCategory+
 �� " �$ PropertyChanged=��� $��+��$��1��$��0,5,0,0q����� * DataContext.addCategoryCommand+
 �� " ��� $��-��$��2��$��0,5,0,0q����� *#!DataContext.deleteCategoryCommand+
 �� " ��� $��Ред.��$��3��$��0,5,0,0q����� *!DataContext.editCategoryCommand+
 �� " �]� ��.  $��1��.+��� $��Добавить��$��	5,0,10,10q�$	��Top=����� *addContactCommand+�� $ ��Редактировать��$��
10,0,10,10q�$	��Top=����� *editContactCommand+�� $��Удалить��$��	10,0,10,0q�$	��Top=����� *deleteContactCommand+���� *SelectedContact+�� $
��Left=�$��5,0,0,91q�$��1��$
��Wrap=�$	��134������ *StatusString+�  �� $	��11* ��� $
��124* �.+��� $��1������ *ContactsList+$ PropertyChanged=� �� *SelectedContact+���� .R��� $��5q�.+��� ����  �� Path$	 FIO��� ���� $ mobile��� ���� $ address��� $%��Добавить Контакт������ *clearInputCommand+