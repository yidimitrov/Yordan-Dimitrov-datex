Description:

In a warehouse we have pallets that can have boxes on them and we have boxes that can have other boxes inside. Boxes have alphanumeric barcodes to identify them. In a relational database we will have two tables representing boxes and pallets.

The relation between a pallet and a box is only with the boxes that are not inside other boxes (top level).

A warehouse worker will go to one pallet and take some of the boxes. In some cases he may open a box to take an inner box. An opened box will be thrown away and the inner boxes will stay on the pallet.

At the end of the process he will report the barcodes of all the boxes he took.


Solution:

- Model your database and generate some valid data in it. For this example don't need to have more than 3 levels of nested boxes. Create all the necessary database objects to guarantee correct data and efficient DML operations.

- Write a program that will take as input a list of barcodes corresponding to the taken boxes and will automatically update your database to reflect all the changes you physically did during the process described above.

- Write appropriate unit tests for your algorithm.

  
Example:

Before:
Pallet P1

      Box 'BC1'

            Box 'BC2'

            Box 'BC3'

      Box 'BC4'

            Box 'BC5'

            Box 'BC6'

            Box 'BC7'

 
Worker takes 'BC1' and 'BC6'.

After:
Pallet P1

      Box 'BC5'

      Box 'BC7'
