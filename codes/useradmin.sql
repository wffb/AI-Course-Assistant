/*
 Navicat Premium Data Transfer

 Source Server         : SchoolProject_ai
 Source Server Type    : MySQL
 Source Server Version : 50726
 Source Host           : localhost:3306
 Source Schema         : useradmin

 Target Server Type    : MySQL
 Target Server Version : 50726
 File Encoding         : 65001

 Date: 12/10/2024 15:08:46
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for ai_agent
-- ----------------------------
DROP TABLE IF EXISTS `ai_agent`;
CREATE TABLE `ai_agent`  (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `subject_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `instructions` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `model` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `identify_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `temperature` double NOT NULL,
  `top_p` double NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_subject_id`(`subject_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of ai_agent
-- ----------------------------
INSERT INTO `ai_agent` VALUES ('2e219d93c3eb4d7bf770cabcc8e75af2', 'assistant robot for 90014', '90014', 'This is an AI assistant that can help you with software practise project', 'You are an AI assistant that can help students with software management. You should provide concise, understandable and correct answers to questions about software practise project.', 'gpt-4o', 'asst_UR6xCV6RwBxaNoVF32ZKC7N4', 0, 1);
INSERT INTO `ai_agent` VALUES ('8bc97adb2aa927b3621816b94d73a0f5', 'assistant robot for 80011', '80011', 'This is an AI assistant that can help you with chemical design safety.', 'You will function as a teaching tool to help 2 nd year undergraduate chemical engineering students learn about safety.', 'gpt-4o', 'asst_Hb2xc6GmRGyas9EU3obu4urt', 0, 1);
INSERT INTO `ai_agent` VALUES ('e69d700bb89ae5827d6e379acd611739', 'assistant robot for 90015', '90015', 'This is an AI assistant that can help you with database design', 'You are an AI assistant that can help students with database design. You should provide concise, understandable and correct answers to questions about database design.', 'gpt-4o', 'asst_3T4aRVSCs3UZnj5WWk9go5ib', 0, 1);
INSERT INTO `ai_agent` VALUES ('f5ad719ca5b7bef7e937e709b0d6c8ca', 'assistant robot for 90016', '90016', 'This is an AI assistant that can help you with software management', 'You are an AI assistant that can help students with software management. You should provide concise, understandable and correct answers to questions about software management.', 'gpt-4o', 'asst_fSsHVPmVv7uR26d6fPWoqrBO', 1, 1);

-- ----------------------------
-- Table structure for ai_file
-- ----------------------------
DROP TABLE IF EXISTS `ai_file`;
CREATE TABLE `ai_file`  (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `file_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `date` date NOT NULL,
  `teacher_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `assistant_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of ai_file
-- ----------------------------
INSERT INTO `ai_file` VALUES ('file-9kT240fcvxcKvs85YT1r9Bjp', '2024_10_12_11_51_35__Operator Training Simulator System Prompt Scenario 1.docx', '2024-10-12', NULL, 'asst_Hb2xc6GmRGyas9EU3obu4urt');

-- ----------------------------
-- Table structure for interactive_journal
-- ----------------------------
DROP TABLE IF EXISTS `interactive_journal`;
CREATE TABLE `interactive_journal`  (
  `student_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `subject_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `date` date NOT NULL,
  `file_name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`student_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of interactive_journal
-- ----------------------------

-- ----------------------------
-- Table structure for student
-- ----------------------------
DROP TABLE IF EXISTS `student`;
CREATE TABLE `student`  (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `password` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `subjects` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of student
-- ----------------------------

-- ----------------------------
-- Table structure for student_info
-- ----------------------------
DROP TABLE IF EXISTS `student_info`;
CREATE TABLE `student_info`  (
  `id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `subjects` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of student_info
-- ----------------------------
INSERT INTO `student_info` VALUES ('1', '0640f41cba7a277041c79ea263642d17', '90016/90014');
INSERT INTO `student_info` VALUES ('2', '7d39dd69a0f9da26d3f77b0d87aab7f7', '90015/80011');

-- ----------------------------
-- Table structure for subject
-- ----------------------------
DROP TABLE IF EXISTS `subject`;
CREATE TABLE `subject`  (
  `id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `identify_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `description` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_identify_id`(`identify_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of subject
-- ----------------------------
INSERT INTO `subject` VALUES ('1', 'software management', '90016', 'a subject about management');
INSERT INTO `subject` VALUES ('2', 'practise project', '90014', 'a subject about practise');
INSERT INTO `subject` VALUES ('3', 'database design', '90015', 'a subject about database design');
INSERT INTO `subject` VALUES ('4', 'chemical  engineering safety', '80011', 'a subject about chemical  engineering safety');

-- ----------------------------
-- Table structure for teacher
-- ----------------------------
DROP TABLE IF EXISTS `teacher`;
CREATE TABLE `teacher`  (
  `teacher_id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `teacher_name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `password` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `role` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `department` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`teacher_id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = DYNAMIC;

-- ----------------------------
-- Records of teacher
-- ----------------------------

-- ----------------------------
-- Table structure for teacher_info
-- ----------------------------
DROP TABLE IF EXISTS `teacher_info`;
CREATE TABLE `teacher_info`  (
  `id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `department` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `role` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `user_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of teacher_info
-- ----------------------------

-- ----------------------------
-- Table structure for user
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user`  (
  `username` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `is_teacher` tinyint(1) NOT NULL,
  `info_id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT '',
  `id` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `index_username`(`username`) USING BTREE
) ENGINE = MyISAM CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of user
-- ----------------------------
INSERT INTO `user` VALUES ('user1', '$2a$10$r.RdCOcjCsCB0EbSwL3soeqITEZPEUS39f9K90IytkOZ57yTAez7G', 0, '', '0640f41cba7a277041c79ea263642d17');
INSERT INTO `user` VALUES ('user2', '$2a$10$gp5iHf04VrUFDFQL1JNCTO.v0iNDzrZM13/fmMrYJpAi.Kmc4THNa', 0, '', '7d39dd69a0f9da26d3f77b0d87aab7f7');
INSERT INTO `user` VALUES ('root', '$2a$10$pmquuowDMFf9sXyRfVGrjuPVwo6Nr95M9Tlg1z6os4QSNOnJXUx2S', 1, '', 'd80382edf24860d4aef805832bb261bb');

SET FOREIGN_KEY_CHECKS = 1;
