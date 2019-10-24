/*
SQLyog Community Edition- MySQL GUI v8.13 RC
MySQL - 5.0.22-community-nt : Database - mstest
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`mstest` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mstest`;

/*Table structure for table `college` */

DROP TABLE IF EXISTS `college`;

CREATE TABLE `college` (
  `Id` varchar(50) default NULL,
  `Top` int(11) default NULL,
  `SchoolName` varchar(500) default NULL,
  `SchoolSplace` varchar(500) default NULL,
  `SchoolLifeScore` decimal(10,0) default NULL,
  `Type` varchar(500) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `college` */

insert  into `college`(`Id`,`Top`,`SchoolName`,`SchoolSplace`,`SchoolLifeScore`,`Type`) values ('20c95eb4-9f64-47a8-8070-b50c49586021',1,'清华大学','北京','100',NULL),('a477add6-c117-4fe5-a60d-87388c2a04d3',2,'北京大学','北京','95',NULL),('032a7323-0861-40fb-98ec-66710c022415',3,'复旦大学','上海','92',NULL),('e8d6cd7b-7ef4-4cc0-b973-bf97c1fdd0cd',4,'上海交通大学','上海','91',NULL),('144495ec-5a7f-4ba3-9e47-9729674f08aa',5,'中国科学技术大学','安徽','91',NULL),('8e254914-6b01-4fc2-a5dd-33fb92ec355a',6,'中国人民大学','北京','88',NULL),('99122bdc-068c-48a3-8dea-9dade33989c3',7,'北京航空航天大学','北京','87',NULL),('e0f25aa6-2534-4434-bdc3-ebf2405a042c',8,'南京大学','江苏','86',NULL),('34a47171-efa5-47d5-aadb-c86b046b0645',9,'同济大学','上海','85',NULL),('7403068a-f28f-4b11-95d0-ac1ee4d4e5cb',10,'浙江大学','浙江','84',NULL),('7cbfbe01-2e71-47f5-8693-0d5ca141b341',11,'南开大学','天津','84',NULL),('f96ae51c-47d4-49cc-a14b-e57191e0312a',12,'北京师范大学','北京','83',NULL),('a12d3954-8a92-4965-a953-7fd578441033',13,'武汉大学','湖北','82',NULL),('24a3b0e0-fee1-4391-ae6d-aba4be1f59a4',14,'西安交通大学','陕西','82',NULL),('30bf7f0e-8e3a-4544-a3b4-c85802c2162d',15,'华中科技大学','湖北','80',NULL),('f97c56b7-1dc4-4944-8704-0e3e4bdcf877',16,'天津大学','天津','80',NULL),('602d3390-a637-45b9-8b79-1a909f7bd34b',17,'中山大学','广东','80',NULL),('6abe4ce6-664b-47d2-9ac0-27ddb5bcc2f3',18,'北京理工大学','北京','80',NULL),('b50814a5-373d-46de-bb65-58632b6270a5',19,'东南大学','江苏','79',NULL),('ec961c07-1208-4e94-82e6-073e7d06a281',20,'华东师范大学','上海','78',NULL),('24edb705-36d1-4091-93bb-6ec4097499b4',21,'哈尔滨工业大学','黑龙江','77',NULL),('fef47eb9-5e6a-4608-ae29-0a6c33da55d5',22,'厦门大学','福建','75',NULL),('6551ef3f-dc78-43ee-942d-a85dd935cc4f',23,'四川大学','四川','74',NULL),('bad050d8-fbbb-40b6-8dfa-49ebd91eb107',24,'西北工业大学','陕西','74',NULL),('448fd45f-ec7d-46fa-954b-0e474ac7a1bf',25,'电子科技大学','四川','74',NULL),('25b1da56-be53-4b3f-932f-1dfa5768fa8f',26,'华南理工大学','广东','73',NULL),('d1dbaadd-9869-44ba-bade-ee4ae1143097',27,'中南大学','湖南','73',NULL),('893a8460-2244-4316-9e01-1f36fa313b91',28,'大连理工大学','辽宁','73',NULL),('24fa029f-eb72-4e9a-ae82-a6782c561857',29,'吉林大学','吉林','72',NULL),('c7dbff2e-e563-43ae-b6e6-cfd10431fee7',30,'湖南大学','湖南','72',NULL),('c364794c-f65d-4fc0-b1e4-99e0b9d57b7b',31,'山东大学','山东','71',NULL),('7679576c-3ce8-4523-80af-6f36e79a5379',32,'重庆大学','重庆','71',NULL),('26e9b029-2162-4395-bfe4-78386f65e191',33,'中国农业大学','北京','69',NULL),('1289c614-c9df-4653-ad47-66e0ce96429a',34,'中国海洋大学','山东','68',NULL),('4c55fc9c-801e-4062-b401-657ac610a1be',35,'中央民族大学','北京','68',NULL),('2a36351f-d93a-4932-842d-6d73d5d021d5',36,'东北大学','辽宁','67',NULL),('4ee72967-6da9-4458-b198-330a092643a2',37,'兰州大学','甘肃','66',NULL),('bf42d58f-afae-4895-969d-10acfe8391e0',38,'西北农林科技大学','陕西','61',NULL);

/*Table structure for table `ms_department` */

DROP TABLE IF EXISTS `ms_department`;

CREATE TABLE `ms_department` (
  `Id` varchar(50) NOT NULL COMMENT '主键，使用GUID',
  `DepartmentNo` varchar(10) default NULL COMMENT '部门编码',
  `DepartmentName` varchar(20) default NULL COMMENT '部门姓名',
  `Remarks` varchar(100) default NULL COMMENT '备注',
  PRIMARY KEY  (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `ms_department` */

insert  into `ms_department`(`Id`,`DepartmentNo`,`DepartmentName`,`Remarks`) values ('96ef3415-9d24-4e6f-a49a-d1bf4536dd9d','SZ_001','.Net开发组','负责客户端产品'),('6fed61b4-122e-4b92-9b14-803cd809c206','SZ_002','Java开发组','负责平台产品'),('45a8c69e-4ea0-45b7-9352-57f821e123f8','SZ_003','测试组','负责产品的测试'),('4c4c118b-cd75-4038-b660-32f0a9a0763f','SZ_004','运维组','负责产品的安装和维护'),('ca10bf1e-beeb-40f4-a8ba-e7d55310dc8d','SZ_005','技术支持','负责产品使用的技术指导');

/*Table structure for table `ms_empoloyee` */

DROP TABLE IF EXISTS `ms_empoloyee`;

CREATE TABLE `ms_empoloyee` (
  `Id` varchar(50) NOT NULL COMMENT '主键',
  `DepartmentId` varchar(50) default NULL COMMENT '外键 关联部门表',
  `EmployeeNo` varchar(10) default NULL COMMENT '员工编码',
  `EmployeeName` varchar(20) default NULL COMMENT '员工姓名',
  `EmployeeSex` varchar(10) default NULL COMMENT '员工性别',
  `EmployeeBirth` datetime default NULL COMMENT '员工生日',
  `IsJob` tinyint(1) default NULL COMMENT '是否在职',
  `Remarks` varchar(100) default NULL COMMENT '备注',
  PRIMARY KEY  (`Id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Data for the table `ms_empoloyee` */

insert  into `ms_empoloyee`(`Id`,`DepartmentId`,`EmployeeNo`,`EmployeeName`,`EmployeeSex`,`EmployeeBirth`,`IsJob`,`Remarks`) values ('8a6fce77-aa63-4560-a3d3-860a56e07210','96ef3415-9d24-4e6f-a49a-d1bf4536dd9d','Net001','张三','男','1990-01-01 00:00:00',0,NULL),('a7cb59bf-7bea-4cb2-8d09-dd0be0143706','6fed61b4-122e-4b92-9b14-803cd809c206','Java001','黄小五','女','1990-05-09 00:00:00',-1,''),('c0e54d1c-c5fb-451e-8f7b-98ccee5b4d0e','6fed61b4-122e-4b92-9b14-803cd809c206','Java002','欧阳天天','男','1993-10-01 00:00:00',-1,''),('e1a9986c-e045-4d8e-9a72-c92c1b82bed9','45a8c69e-4ea0-45b7-9352-57f821e123f8','Cs001','李四','男','1994-02-02 00:00:00',1,''),('b25f637d-2528-4486-8c04-635f633b939a','ca10bf1e-beeb-40f4-a8ba-e7d55310dc8d','JSZC001','李明','女','2019-10-12 00:00:00',1,NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
