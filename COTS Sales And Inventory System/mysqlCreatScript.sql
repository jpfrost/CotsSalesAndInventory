-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
SHOW WARNINGS;
-- -----------------------------------------------------
-- Schema cotsalesinventory
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `cotsalesinventory` ;

-- -----------------------------------------------------
-- Schema cotsalesinventory
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `cotsalesinventory` DEFAULT CHARACTER SET utf8 ;
SHOW WARNINGS;
USE `cotsalesinventory` ;

-- -----------------------------------------------------
-- Table `account`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `account` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `account` (
  `AccountID` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `AccountName` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `AccountPassword` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `AccountType` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`AccountID`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `date`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `date` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `date` (
  `DateID` INT(11) NOT NULL COMMENT '',
  `Date` DATETIME NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`DateID`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `category`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `category` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `category` (
  `CategoryID` INT(11) NOT NULL COMMENT '',
  `CategoryName` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`CategoryID`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `items`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `items` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `items` (
  `ItemID` VARCHAR(15) NOT NULL DEFAULT '0' COMMENT '',
  `CategoryID` INT(11) NULL DEFAULT NULL COMMENT '',
  `Item_Name` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`ItemID`)  COMMENT '',
  INDEX `fk_Inventory_Category1_idx` (`CategoryID` ASC)  COMMENT '',
  CONSTRAINT `fk_Inventory_Category1`
    FOREIGN KEY (`CategoryID`)
    REFERENCES `category` (`CategoryID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `backorder`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `backorder` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `backorder` (
  `BackOrderID` INT(11) NOT NULL COMMENT '',
  `ItemID` VARCHAR(15) NOT NULL COMMENT '',
  `Date_DateID` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`BackOrderID`)  COMMENT '',
  INDEX `fk_BackOrder_Inventory1_idx` (`ItemID` ASC)  COMMENT '',
  INDEX `fk_BackOrder_Date1_idx` (`Date_DateID` ASC)  COMMENT '',
  CONSTRAINT `fk_BackOrder_Date1`
    FOREIGN KEY (`Date_DateID`)
    REFERENCES `date` (`DateID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_BackOrder_Inventory1`
    FOREIGN KEY (`ItemID`)
    REFERENCES `items` (`ItemID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `customer`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `customer` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `customer` (
  `custID` INT(11) NOT NULL COMMENT '',
  `custName` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `custEmail` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`custID`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `distributor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `distributor` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `distributor` (
  `DistroID` INT(11) NOT NULL COMMENT '',
  `DistroName` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `DistroAddress` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `DistroContact` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `distroEnable` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`DistroID`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `size`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `size` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `size` (
  `SizeID` INT(11) NOT NULL AUTO_INCREMENT COMMENT '',
  `ItemID` VARCHAR(15) NOT NULL COMMENT '',
  `Size` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `Price` DOUBLE NULL DEFAULT NULL COMMENT '',
  `Quantity` INT(11) NULL DEFAULT NULL COMMENT '',
  `sizeEnable` INT(11) NULL DEFAULT '1' COMMENT '',
  PRIMARY KEY (`SizeID`)  COMMENT '',
  INDEX `fk_Size_Items1_idx` (`ItemID` ASC)  COMMENT '',
  CONSTRAINT `fk_Size_Items1`
    FOREIGN KEY (`ItemID`)
    REFERENCES `items` (`ItemID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `itemlevel`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `itemlevel` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `itemlevel` (
  `itemLevelId` INT(11) NOT NULL COMMENT '',
  `SizeID` INT(11) NULL DEFAULT NULL COMMENT '',
  `itemMinLevel` INT(11) NULL DEFAULT NULL COMMENT '',
  `itemMaxLevel` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`itemLevelId`)  COMMENT '',
  INDEX `fk_itemlevel_size1_idx` (`SizeID` ASC)  COMMENT '',
  CONSTRAINT `fk_itemlevel_size1`
    FOREIGN KEY (`SizeID`)
    REFERENCES `size` (`SizeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `items_seq`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `items_seq` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `items_seq` (
  `id` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`id`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `orderlist`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `orderlist` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `orderlist` (
  `idorderList` INT(11) NOT NULL COMMENT '',
  `orderdesc` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `orderDelivered` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`idorderList`)  COMMENT '')
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `orders`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `orders` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `orders` (
  `OrderID` INT(11) NOT NULL COMMENT '',
  `DateID` INT(11) NOT NULL COMMENT '',
  `DistroID` INT(11) NOT NULL COMMENT '',
  `AccountID` INT(11) NULL DEFAULT NULL COMMENT '',
  `OrderQty` VARCHAR(45) NULL DEFAULT NULL COMMENT '',
  `idorderList` INT(11) NOT NULL COMMENT '',
  `SizeID` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`OrderID`)  COMMENT '',
  INDEX `fk_tblOrder_date1_idx` (`DateID` ASC)  COMMENT '',
  INDEX `fk_tblOrder_distributor1_idx` (`DistroID` ASC)  COMMENT '',
  INDEX `fk_orderlist_account1_idx` (`AccountID` ASC)  COMMENT '',
  INDEX `fk_orders_orderList1_idx` (`idorderList` ASC)  COMMENT '',
  INDEX `fk_orders_size1_idx` (`SizeID` ASC)  COMMENT '',
  CONSTRAINT `fk_orderlist_account1`
    FOREIGN KEY (`AccountID`)
    REFERENCES `account` (`AccountID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_orders_orderList1`
    FOREIGN KEY (`idorderList`)
    REFERENCES `orderlist` (`idorderList`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_orders_size1`
    FOREIGN KEY (`SizeID`)
    REFERENCES `size` (`SizeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tblOrder_date1`
    FOREIGN KEY (`DateID`)
    REFERENCES `date` (`DateID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tblOrder_distributor1`
    FOREIGN KEY (`DistroID`)
    REFERENCES `distributor` (`DistroID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `orderlistitems`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `orderlistitems` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `orderlistitems` (
  `OrderID` INT(11) NOT NULL COMMENT '',
  `ItemID` VARCHAR(15) NOT NULL COMMENT '',
  PRIMARY KEY (`OrderID`, `ItemID`)  COMMENT '',
  INDEX `fk_OrderList_has_Items_Items1_idx` (`ItemID` ASC)  COMMENT '',
  INDEX `fk_OrderList_has_Items_OrderList1_idx` (`OrderID` ASC)  COMMENT '',
  CONSTRAINT `fk_OrderList_has_Items_Items1`
    FOREIGN KEY (`ItemID`)
    REFERENCES `items` (`ItemID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_OrderList_has_Items_OrderList1`
    FOREIGN KEY (`OrderID`)
    REFERENCES `orders` (`OrderID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `receiptid`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `receiptid` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `receiptid` (
  `ReceiptID` INT(11) NOT NULL COMMENT '',
  `AccountID` INT(11) NULL DEFAULT NULL COMMENT '',
  `DateID` INT(11) NULL DEFAULT NULL COMMENT '',
  `customer_custID` INT(11) NULL DEFAULT NULL COMMENT '',
  PRIMARY KEY (`ReceiptID`)  COMMENT '',
  INDEX `fk_ReceiptID_Account_idx` (`AccountID` ASC)  COMMENT '',
  INDEX `fk_ReceiptID_Date1_idx` (`DateID` ASC)  COMMENT '',
  INDEX `fk_receiptid_customer1_idx` (`customer_custID` ASC)  COMMENT '',
  CONSTRAINT `fk_ReceiptID_Account`
    FOREIGN KEY (`AccountID`)
    REFERENCES `account` (`AccountID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ReceiptID_Date1`
    FOREIGN KEY (`DateID`)
    REFERENCES `date` (`DateID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_receiptid_customer1`
    FOREIGN KEY (`customer_custID`)
    REFERENCES `customer` (`custID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `sale`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `sale` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `sale` (
  `SaleID` INT(11) NOT NULL COMMENT '',
  `ReceiptID` INT(11) NOT NULL COMMENT '',
  `Count` INT(11) NULL DEFAULT NULL COMMENT '',
  `SizeID` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`SaleID`)  COMMENT '',
  INDEX `fk_Sale_ReceiptID1_idx` (`ReceiptID` ASC)  COMMENT '',
  INDEX `fk_sale_Size1_idx` (`SizeID` ASC)  COMMENT '',
  CONSTRAINT `fk_Sale_ReceiptID1`
    FOREIGN KEY (`ReceiptID`)
    REFERENCES `receiptid` (`ReceiptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_sale_Size1`
    FOREIGN KEY (`SizeID`)
    REFERENCES `size` (`SizeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tblconvert`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tblconvert` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tblconvert` (
  `convertID` INT(11) NOT NULL COMMENT '',
  `InitialSize` INT(11) NOT NULL COMMENT '',
  `convertSize` INT(11) NOT NULL COMMENT '',
  `convertQuantity` INT(11) NOT NULL COMMENT '',
  PRIMARY KEY (`convertID`)  COMMENT '',
  INDEX `fk_convert_Size1_idx` (`InitialSize` ASC)  COMMENT '',
  INDEX `fk_convert_Size2_idx` (`convertSize` ASC)  COMMENT '',
  CONSTRAINT `fk_convert_Size1`
    FOREIGN KEY (`InitialSize`)
    REFERENCES `size` (`SizeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_convert_Size2`
    FOREIGN KEY (`convertSize`)
    REFERENCES `size` (`SizeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

SHOW WARNINGS;

-- -----------------------------------------------------
-- Table `tblsizes`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `tblsizes` ;

SHOW WARNINGS;
CREATE TABLE IF NOT EXISTS `tblsizes` (
  `sizesID` INT NOT NULL COMMENT '',
  `sizesName` VARCHAR(45) NULL COMMENT '',
  `CategoryID` INT(11) NULL COMMENT '',
  PRIMARY KEY (`sizesID`)  COMMENT '',
  INDEX `fk_tblsizes_category1_idx` (`CategoryID` ASC)  COMMENT '',
  CONSTRAINT `fk_tblsizes_category1`
    FOREIGN KEY (`CategoryID`)
    REFERENCES `category` (`CategoryID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

SHOW WARNINGS;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
