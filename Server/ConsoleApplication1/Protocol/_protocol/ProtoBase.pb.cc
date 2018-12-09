// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: ProtoBase.proto

#include "ProtoBase.pb.h"

#include <algorithm>

#include <google/protobuf/stubs/common.h>
#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/extension_set.h>
#include <google/protobuf/wire_format_lite_inl.h>
#include <google/protobuf/descriptor.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/reflection_ops.h>
#include <google/protobuf/wire_format.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>

namespace Proto {
class ProtoBaseCmdDefaultTypeInternal {
 public:
  ::google::protobuf::internal::ExplicitlyConstructed<ProtoBaseCmd> _instance;
} _ProtoBaseCmd_default_instance_;
class CMD_TESTDefaultTypeInternal {
 public:
  ::google::protobuf::internal::ExplicitlyConstructed<CMD_TEST> _instance;
} _CMD_TEST_default_instance_;
}  // namespace Proto
static void InitDefaultsProtoBaseCmd_ProtoBase_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::Proto::_ProtoBaseCmd_default_instance_;
    new (ptr) ::Proto::ProtoBaseCmd();
    ::google::protobuf::internal::OnShutdownDestroyMessage(ptr);
  }
  ::Proto::ProtoBaseCmd::InitAsDefaultInstance();
}

::google::protobuf::internal::SCCInfo<0> scc_info_ProtoBaseCmd_ProtoBase_2eproto =
    {{ATOMIC_VAR_INIT(::google::protobuf::internal::SCCInfoBase::kUninitialized), 0, InitDefaultsProtoBaseCmd_ProtoBase_2eproto}, {}};

static void InitDefaultsCMD_TEST_ProtoBase_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::Proto::_CMD_TEST_default_instance_;
    new (ptr) ::Proto::CMD_TEST();
    ::google::protobuf::internal::OnShutdownDestroyMessage(ptr);
  }
  ::Proto::CMD_TEST::InitAsDefaultInstance();
}

::google::protobuf::internal::SCCInfo<0> scc_info_CMD_TEST_ProtoBase_2eproto =
    {{ATOMIC_VAR_INIT(::google::protobuf::internal::SCCInfoBase::kUninitialized), 0, InitDefaultsCMD_TEST_ProtoBase_2eproto}, {}};

void InitDefaults_ProtoBase_2eproto() {
  ::google::protobuf::internal::InitSCC(&scc_info_ProtoBaseCmd_ProtoBase_2eproto.base);
  ::google::protobuf::internal::InitSCC(&scc_info_CMD_TEST_ProtoBase_2eproto.base);
}

::google::protobuf::Metadata file_level_metadata_ProtoBase_2eproto[2];
const ::google::protobuf::EnumDescriptor* file_level_enum_descriptors_ProtoBase_2eproto[1];
constexpr ::google::protobuf::ServiceDescriptor const** file_level_service_descriptors_ProtoBase_2eproto = nullptr;

const ::google::protobuf::uint32 TableStruct_ProtoBase_2eproto::offsets[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  PROTOBUF_FIELD_OFFSET(::Proto::ProtoBaseCmd, _has_bits_),
  PROTOBUF_FIELD_OFFSET(::Proto::ProtoBaseCmd, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::Proto::ProtoBaseCmd, cmdhead_),
  PROTOBUF_FIELD_OFFSET(::Proto::ProtoBaseCmd, buffer_),
  1,
  0,
  PROTOBUF_FIELD_OFFSET(::Proto::CMD_TEST, _has_bits_),
  PROTOBUF_FIELD_OFFSET(::Proto::CMD_TEST, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::Proto::CMD_TEST, msg_),
  0,
};
static const ::google::protobuf::internal::MigrationSchema schemas[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  { 0, 7, sizeof(::Proto::ProtoBaseCmd)},
  { 9, 15, sizeof(::Proto::CMD_TEST)},
};

static ::google::protobuf::Message const * const file_default_instances[] = {
  reinterpret_cast<const ::google::protobuf::Message*>(&::Proto::_ProtoBaseCmd_default_instance_),
  reinterpret_cast<const ::google::protobuf::Message*>(&::Proto::_CMD_TEST_default_instance_),
};

::google::protobuf::internal::AssignDescriptorsTable assign_descriptors_table_ProtoBase_2eproto = {
  {}, AddDescriptors_ProtoBase_2eproto, "ProtoBase.proto", schemas,
  file_default_instances, TableStruct_ProtoBase_2eproto::offsets,
  file_level_metadata_ProtoBase_2eproto, 2, file_level_enum_descriptors_ProtoBase_2eproto, file_level_service_descriptors_ProtoBase_2eproto,
};

const char descriptor_table_protodef_ProtoBase_2eproto[] =
  "\n\017ProtoBase.proto\022\005Proto\"D\n\014ProtoBaseCmd"
  "\022$\n\007CmdHead\030\001 \002(\0162\023.Proto.ProtoCommand\022\016"
  "\n\006buffer\030\002 \002(\014\"\027\n\010CMD_TEST\022\013\n\003msg\030\001 \001(\014*"
  "*\n\014ProtoCommand\022\032\n\026ProtoCommand_TestMode"
  "l\020\001"
  ;
::google::protobuf::internal::DescriptorTable descriptor_table_ProtoBase_2eproto = {
  false, InitDefaults_ProtoBase_2eproto, 
  descriptor_table_protodef_ProtoBase_2eproto,
  "ProtoBase.proto", &assign_descriptors_table_ProtoBase_2eproto, 163,
};

void AddDescriptors_ProtoBase_2eproto() {
  static constexpr ::google::protobuf::internal::InitFunc deps[1] =
  {
  };
 ::google::protobuf::internal::AddDescriptors(&descriptor_table_ProtoBase_2eproto, deps, 0);
}

// Force running AddDescriptors() at dynamic initialization time.
static bool dynamic_init_dummy_ProtoBase_2eproto = []() { AddDescriptors_ProtoBase_2eproto(); return true; }();
namespace Proto {
const ::google::protobuf::EnumDescriptor* ProtoCommand_descriptor() {
  ::google::protobuf::internal::AssignDescriptors(&assign_descriptors_table_ProtoBase_2eproto);
  return file_level_enum_descriptors_ProtoBase_2eproto[0];
}
bool ProtoCommand_IsValid(int value) {
  switch (value) {
    case 1:
      return true;
    default:
      return false;
  }
}


// ===================================================================

void ProtoBaseCmd::InitAsDefaultInstance() {
}
class ProtoBaseCmd::HasBitSetters {
 public:
  static void set_has_cmdhead(ProtoBaseCmd* msg) {
    msg->_has_bits_[0] |= 0x00000002u;
  }
  static void set_has_buffer(ProtoBaseCmd* msg) {
    msg->_has_bits_[0] |= 0x00000001u;
  }
};

#if !defined(_MSC_VER) || _MSC_VER >= 1900
const int ProtoBaseCmd::kCmdHeadFieldNumber;
const int ProtoBaseCmd::kBufferFieldNumber;
#endif  // !defined(_MSC_VER) || _MSC_VER >= 1900

ProtoBaseCmd::ProtoBaseCmd()
  : ::google::protobuf::Message(), _internal_metadata_(nullptr) {
  SharedCtor();
  // @@protoc_insertion_point(constructor:Proto.ProtoBaseCmd)
}
ProtoBaseCmd::ProtoBaseCmd(const ProtoBaseCmd& from)
  : ::google::protobuf::Message(),
      _internal_metadata_(nullptr),
      _has_bits_(from._has_bits_) {
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  buffer_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  if (from.has_buffer()) {
    buffer_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.buffer_);
  }
  cmdhead_ = from.cmdhead_;
  // @@protoc_insertion_point(copy_constructor:Proto.ProtoBaseCmd)
}

void ProtoBaseCmd::SharedCtor() {
  ::google::protobuf::internal::InitSCC(
      &scc_info_ProtoBaseCmd_ProtoBase_2eproto.base);
  buffer_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  cmdhead_ = 1;
}

ProtoBaseCmd::~ProtoBaseCmd() {
  // @@protoc_insertion_point(destructor:Proto.ProtoBaseCmd)
  SharedDtor();
}

void ProtoBaseCmd::SharedDtor() {
  buffer_.DestroyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}

void ProtoBaseCmd::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const ProtoBaseCmd& ProtoBaseCmd::default_instance() {
  ::google::protobuf::internal::InitSCC(&::scc_info_ProtoBaseCmd_ProtoBase_2eproto.base);
  return *internal_default_instance();
}


void ProtoBaseCmd::Clear() {
// @@protoc_insertion_point(message_clear_start:Proto.ProtoBaseCmd)
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  if (cached_has_bits & 0x00000003u) {
    if (cached_has_bits & 0x00000001u) {
      buffer_.ClearNonDefaultToEmptyNoArena();
    }
    cmdhead_ = 1;
  }
  _has_bits_.Clear();
  _internal_metadata_.Clear();
}

#if GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
const char* ProtoBaseCmd::_InternalParse(const char* begin, const char* end, void* object,
                  ::google::protobuf::internal::ParseContext* ctx) {
  auto msg = static_cast<ProtoBaseCmd*>(object);
  ::google::protobuf::uint32 size; (void)size;
  int depth; (void)depth;
  ::google::protobuf::uint32 tag;
  ::google::protobuf::internal::ParseFunc parser_till_end; (void)parser_till_end;
  auto ptr = begin;
  while (ptr < end) {
    ptr = Varint::Parse32Inline(ptr, &tag);
    GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
    switch (tag >> 3) {
      // required .Proto.ProtoCommand CmdHead = 1;
      case 1: {
        if (static_cast<::google::protobuf::uint8>(tag) != 8) goto handle_unusual;
        ::google::protobuf::uint64 val;
        ptr = Varint::Parse64(ptr, &val);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        if (!::Proto::ProtoCommand_IsValid(val)) {
          ::google::protobuf::internal::WriteVarint(1, val, msg->mutable_unknown_fields());
          break;
        }
        ::Proto::ProtoCommand value = static_cast<::Proto::ProtoCommand>(val);
        msg->set_cmdhead(value);
        break;
      }
      // required bytes buffer = 2;
      case 2: {
        if (static_cast<::google::protobuf::uint8>(tag) != 18) goto handle_unusual;
        ptr = Varint::Parse32Inline(ptr, &size);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        auto str = msg->mutable_buffer();
        if (size > end - ptr + ::google::protobuf::internal::ParseContext::kSlopBytes) {
          object = str;
          str->clear();
          str->reserve(size);
          parser_till_end = ::google::protobuf::internal::GreedyStringParser;
          goto len_delim_till_end;
        }
        GOOGLE_PROTOBUF_PARSER_ASSERT(::google::protobuf::internal::StringCheck(ptr, size, ctx));
        ::google::protobuf::internal::InlineGreedyStringParser(str, ptr, size, ctx);
        ptr += size;
        break;
      }
      default: {
      handle_unusual: (void)&&handle_unusual;
        if ((tag & 7) == 4 || tag == 0) {
          ctx->EndGroup(tag);
          return ptr;
        }
        auto res = UnknownFieldParse(tag, {_InternalParse, msg},
          ptr, end, msg->_internal_metadata_.mutable_unknown_fields(), ctx);
        ptr = res.first;
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr != nullptr);
        if (res.second) return ptr;
      }
    }  // switch
  }  // while
  return ptr;
len_delim_till_end: (void)&&len_delim_till_end;
  return ctx->StoreAndTailCall(ptr, end, {_InternalParse, msg},
                                 {parser_till_end, object}, size);
group_continues: (void)&&group_continues;
  GOOGLE_DCHECK(ptr >= end);
  GOOGLE_PROTOBUF_PARSER_ASSERT(ctx->StoreGroup({_InternalParse, msg}, {parser_till_end, object}, depth, tag));
  return ptr;
}
#else  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
bool ProtoBaseCmd::MergePartialFromCodedStream(
    ::google::protobuf::io::CodedInputStream* input) {
#define DO_(EXPRESSION) if (!PROTOBUF_PREDICT_TRUE(EXPRESSION)) goto failure
  ::google::protobuf::uint32 tag;
  // @@protoc_insertion_point(parse_start:Proto.ProtoBaseCmd)
  for (;;) {
    ::std::pair<::google::protobuf::uint32, bool> p = input->ReadTagWithCutoffNoLastTag(127u);
    tag = p.first;
    if (!p.second) goto handle_unusual;
    switch (::google::protobuf::internal::WireFormatLite::GetTagFieldNumber(tag)) {
      // required .Proto.ProtoCommand CmdHead = 1;
      case 1: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (8 & 0xFF)) {
          int value = 0;
          DO_((::google::protobuf::internal::WireFormatLite::ReadPrimitive<
                   int, ::google::protobuf::internal::WireFormatLite::TYPE_ENUM>(
                 input, &value)));
          if (::Proto::ProtoCommand_IsValid(value)) {
            set_cmdhead(static_cast< ::Proto::ProtoCommand >(value));
          } else {
            mutable_unknown_fields()->AddVarint(
                1, static_cast<::google::protobuf::uint64>(value));
          }
        } else {
          goto handle_unusual;
        }
        break;
      }

      // required bytes buffer = 2;
      case 2: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (18 & 0xFF)) {
          DO_(::google::protobuf::internal::WireFormatLite::ReadBytes(
                input, this->mutable_buffer()));
        } else {
          goto handle_unusual;
        }
        break;
      }

      default: {
      handle_unusual:
        if (tag == 0) {
          goto success;
        }
        DO_(::google::protobuf::internal::WireFormat::SkipField(
              input, tag, _internal_metadata_.mutable_unknown_fields()));
        break;
      }
    }
  }
success:
  // @@protoc_insertion_point(parse_success:Proto.ProtoBaseCmd)
  return true;
failure:
  // @@protoc_insertion_point(parse_failure:Proto.ProtoBaseCmd)
  return false;
#undef DO_
}
#endif  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER

void ProtoBaseCmd::SerializeWithCachedSizes(
    ::google::protobuf::io::CodedOutputStream* output) const {
  // @@protoc_insertion_point(serialize_start:Proto.ProtoBaseCmd)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  // required .Proto.ProtoCommand CmdHead = 1;
  if (cached_has_bits & 0x00000002u) {
    ::google::protobuf::internal::WireFormatLite::WriteEnum(
      1, this->cmdhead(), output);
  }

  // required bytes buffer = 2;
  if (cached_has_bits & 0x00000001u) {
    ::google::protobuf::internal::WireFormatLite::WriteBytesMaybeAliased(
      2, this->buffer(), output);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    ::google::protobuf::internal::WireFormat::SerializeUnknownFields(
        _internal_metadata_.unknown_fields(), output);
  }
  // @@protoc_insertion_point(serialize_end:Proto.ProtoBaseCmd)
}

::google::protobuf::uint8* ProtoBaseCmd::InternalSerializeWithCachedSizesToArray(
    bool deterministic, ::google::protobuf::uint8* target) const {
  (void)deterministic; // Unused
  // @@protoc_insertion_point(serialize_to_array_start:Proto.ProtoBaseCmd)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  // required .Proto.ProtoCommand CmdHead = 1;
  if (cached_has_bits & 0x00000002u) {
    target = ::google::protobuf::internal::WireFormatLite::WriteEnumToArray(
      1, this->cmdhead(), target);
  }

  // required bytes buffer = 2;
  if (cached_has_bits & 0x00000001u) {
    target =
      ::google::protobuf::internal::WireFormatLite::WriteBytesToArray(
        2, this->buffer(), target);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    target = ::google::protobuf::internal::WireFormat::SerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields(), target);
  }
  // @@protoc_insertion_point(serialize_to_array_end:Proto.ProtoBaseCmd)
  return target;
}

size_t ProtoBaseCmd::RequiredFieldsByteSizeFallback() const {
// @@protoc_insertion_point(required_fields_byte_size_fallback_start:Proto.ProtoBaseCmd)
  size_t total_size = 0;

  if (has_buffer()) {
    // required bytes buffer = 2;
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::BytesSize(
        this->buffer());
  }

  if (has_cmdhead()) {
    // required .Proto.ProtoCommand CmdHead = 1;
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::EnumSize(this->cmdhead());
  }

  return total_size;
}
size_t ProtoBaseCmd::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:Proto.ProtoBaseCmd)
  size_t total_size = 0;

  if (_internal_metadata_.have_unknown_fields()) {
    total_size +=
      ::google::protobuf::internal::WireFormat::ComputeUnknownFieldsSize(
        _internal_metadata_.unknown_fields());
  }
  if (((_has_bits_[0] & 0x00000003) ^ 0x00000003) == 0) {  // All required fields are present.
    // required bytes buffer = 2;
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::BytesSize(
        this->buffer());

    // required .Proto.ProtoCommand CmdHead = 1;
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::EnumSize(this->cmdhead());

  } else {
    total_size += RequiredFieldsByteSizeFallback();
  }
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  int cached_size = ::google::protobuf::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void ProtoBaseCmd::MergeFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:Proto.ProtoBaseCmd)
  GOOGLE_DCHECK_NE(&from, this);
  const ProtoBaseCmd* source =
      ::google::protobuf::DynamicCastToGenerated<ProtoBaseCmd>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:Proto.ProtoBaseCmd)
    ::google::protobuf::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:Proto.ProtoBaseCmd)
    MergeFrom(*source);
  }
}

void ProtoBaseCmd::MergeFrom(const ProtoBaseCmd& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:Proto.ProtoBaseCmd)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  cached_has_bits = from._has_bits_[0];
  if (cached_has_bits & 0x00000003u) {
    if (cached_has_bits & 0x00000001u) {
      _has_bits_[0] |= 0x00000001u;
      buffer_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.buffer_);
    }
    if (cached_has_bits & 0x00000002u) {
      cmdhead_ = from.cmdhead_;
    }
    _has_bits_[0] |= cached_has_bits;
  }
}

void ProtoBaseCmd::CopyFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:Proto.ProtoBaseCmd)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void ProtoBaseCmd::CopyFrom(const ProtoBaseCmd& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:Proto.ProtoBaseCmd)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool ProtoBaseCmd::IsInitialized() const {
  if ((_has_bits_[0] & 0x00000003) != 0x00000003) return false;
  return true;
}

void ProtoBaseCmd::Swap(ProtoBaseCmd* other) {
  if (other == this) return;
  InternalSwap(other);
}
void ProtoBaseCmd::InternalSwap(ProtoBaseCmd* other) {
  using std::swap;
  _internal_metadata_.Swap(&other->_internal_metadata_);
  swap(_has_bits_[0], other->_has_bits_[0]);
  buffer_.Swap(&other->buffer_, &::google::protobuf::internal::GetEmptyStringAlreadyInited(),
    GetArenaNoVirtual());
  swap(cmdhead_, other->cmdhead_);
}

::google::protobuf::Metadata ProtoBaseCmd::GetMetadata() const {
  ::google::protobuf::internal::AssignDescriptors(&::assign_descriptors_table_ProtoBase_2eproto);
  return ::file_level_metadata_ProtoBase_2eproto[kIndexInFileMessages];
}


// ===================================================================

void CMD_TEST::InitAsDefaultInstance() {
}
class CMD_TEST::HasBitSetters {
 public:
  static void set_has_msg(CMD_TEST* msg) {
    msg->_has_bits_[0] |= 0x00000001u;
  }
};

#if !defined(_MSC_VER) || _MSC_VER >= 1900
const int CMD_TEST::kMsgFieldNumber;
#endif  // !defined(_MSC_VER) || _MSC_VER >= 1900

CMD_TEST::CMD_TEST()
  : ::google::protobuf::Message(), _internal_metadata_(nullptr) {
  SharedCtor();
  // @@protoc_insertion_point(constructor:Proto.CMD_TEST)
}
CMD_TEST::CMD_TEST(const CMD_TEST& from)
  : ::google::protobuf::Message(),
      _internal_metadata_(nullptr),
      _has_bits_(from._has_bits_) {
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  msg_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
  if (from.has_msg()) {
    msg_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.msg_);
  }
  // @@protoc_insertion_point(copy_constructor:Proto.CMD_TEST)
}

void CMD_TEST::SharedCtor() {
  ::google::protobuf::internal::InitSCC(
      &scc_info_CMD_TEST_ProtoBase_2eproto.base);
  msg_.UnsafeSetDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}

CMD_TEST::~CMD_TEST() {
  // @@protoc_insertion_point(destructor:Proto.CMD_TEST)
  SharedDtor();
}

void CMD_TEST::SharedDtor() {
  msg_.DestroyNoArena(&::google::protobuf::internal::GetEmptyStringAlreadyInited());
}

void CMD_TEST::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const CMD_TEST& CMD_TEST::default_instance() {
  ::google::protobuf::internal::InitSCC(&::scc_info_CMD_TEST_ProtoBase_2eproto.base);
  return *internal_default_instance();
}


void CMD_TEST::Clear() {
// @@protoc_insertion_point(message_clear_start:Proto.CMD_TEST)
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  if (cached_has_bits & 0x00000001u) {
    msg_.ClearNonDefaultToEmptyNoArena();
  }
  _has_bits_.Clear();
  _internal_metadata_.Clear();
}

#if GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
const char* CMD_TEST::_InternalParse(const char* begin, const char* end, void* object,
                  ::google::protobuf::internal::ParseContext* ctx) {
  auto msg = static_cast<CMD_TEST*>(object);
  ::google::protobuf::uint32 size; (void)size;
  int depth; (void)depth;
  ::google::protobuf::uint32 tag;
  ::google::protobuf::internal::ParseFunc parser_till_end; (void)parser_till_end;
  auto ptr = begin;
  while (ptr < end) {
    ptr = Varint::Parse32Inline(ptr, &tag);
    GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
    switch (tag >> 3) {
      // optional bytes msg = 1;
      case 1: {
        if (static_cast<::google::protobuf::uint8>(tag) != 10) goto handle_unusual;
        ptr = Varint::Parse32Inline(ptr, &size);
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr);
        auto str = msg->mutable_msg();
        if (size > end - ptr + ::google::protobuf::internal::ParseContext::kSlopBytes) {
          object = str;
          str->clear();
          str->reserve(size);
          parser_till_end = ::google::protobuf::internal::GreedyStringParser;
          goto len_delim_till_end;
        }
        GOOGLE_PROTOBUF_PARSER_ASSERT(::google::protobuf::internal::StringCheck(ptr, size, ctx));
        ::google::protobuf::internal::InlineGreedyStringParser(str, ptr, size, ctx);
        ptr += size;
        break;
      }
      default: {
      handle_unusual: (void)&&handle_unusual;
        if ((tag & 7) == 4 || tag == 0) {
          ctx->EndGroup(tag);
          return ptr;
        }
        auto res = UnknownFieldParse(tag, {_InternalParse, msg},
          ptr, end, msg->_internal_metadata_.mutable_unknown_fields(), ctx);
        ptr = res.first;
        GOOGLE_PROTOBUF_PARSER_ASSERT(ptr != nullptr);
        if (res.second) return ptr;
      }
    }  // switch
  }  // while
  return ptr;
len_delim_till_end: (void)&&len_delim_till_end;
  return ctx->StoreAndTailCall(ptr, end, {_InternalParse, msg},
                                 {parser_till_end, object}, size);
group_continues: (void)&&group_continues;
  GOOGLE_DCHECK(ptr >= end);
  GOOGLE_PROTOBUF_PARSER_ASSERT(ctx->StoreGroup({_InternalParse, msg}, {parser_till_end, object}, depth, tag));
  return ptr;
}
#else  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER
bool CMD_TEST::MergePartialFromCodedStream(
    ::google::protobuf::io::CodedInputStream* input) {
#define DO_(EXPRESSION) if (!PROTOBUF_PREDICT_TRUE(EXPRESSION)) goto failure
  ::google::protobuf::uint32 tag;
  // @@protoc_insertion_point(parse_start:Proto.CMD_TEST)
  for (;;) {
    ::std::pair<::google::protobuf::uint32, bool> p = input->ReadTagWithCutoffNoLastTag(127u);
    tag = p.first;
    if (!p.second) goto handle_unusual;
    switch (::google::protobuf::internal::WireFormatLite::GetTagFieldNumber(tag)) {
      // optional bytes msg = 1;
      case 1: {
        if (static_cast< ::google::protobuf::uint8>(tag) == (10 & 0xFF)) {
          DO_(::google::protobuf::internal::WireFormatLite::ReadBytes(
                input, this->mutable_msg()));
        } else {
          goto handle_unusual;
        }
        break;
      }

      default: {
      handle_unusual:
        if (tag == 0) {
          goto success;
        }
        DO_(::google::protobuf::internal::WireFormat::SkipField(
              input, tag, _internal_metadata_.mutable_unknown_fields()));
        break;
      }
    }
  }
success:
  // @@protoc_insertion_point(parse_success:Proto.CMD_TEST)
  return true;
failure:
  // @@protoc_insertion_point(parse_failure:Proto.CMD_TEST)
  return false;
#undef DO_
}
#endif  // GOOGLE_PROTOBUF_ENABLE_EXPERIMENTAL_PARSER

void CMD_TEST::SerializeWithCachedSizes(
    ::google::protobuf::io::CodedOutputStream* output) const {
  // @@protoc_insertion_point(serialize_start:Proto.CMD_TEST)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  // optional bytes msg = 1;
  if (cached_has_bits & 0x00000001u) {
    ::google::protobuf::internal::WireFormatLite::WriteBytesMaybeAliased(
      1, this->msg(), output);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    ::google::protobuf::internal::WireFormat::SerializeUnknownFields(
        _internal_metadata_.unknown_fields(), output);
  }
  // @@protoc_insertion_point(serialize_end:Proto.CMD_TEST)
}

::google::protobuf::uint8* CMD_TEST::InternalSerializeWithCachedSizesToArray(
    bool deterministic, ::google::protobuf::uint8* target) const {
  (void)deterministic; // Unused
  // @@protoc_insertion_point(serialize_to_array_start:Proto.CMD_TEST)
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  cached_has_bits = _has_bits_[0];
  // optional bytes msg = 1;
  if (cached_has_bits & 0x00000001u) {
    target =
      ::google::protobuf::internal::WireFormatLite::WriteBytesToArray(
        1, this->msg(), target);
  }

  if (_internal_metadata_.have_unknown_fields()) {
    target = ::google::protobuf::internal::WireFormat::SerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields(), target);
  }
  // @@protoc_insertion_point(serialize_to_array_end:Proto.CMD_TEST)
  return target;
}

size_t CMD_TEST::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:Proto.CMD_TEST)
  size_t total_size = 0;

  if (_internal_metadata_.have_unknown_fields()) {
    total_size +=
      ::google::protobuf::internal::WireFormat::ComputeUnknownFieldsSize(
        _internal_metadata_.unknown_fields());
  }
  ::google::protobuf::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // optional bytes msg = 1;
  cached_has_bits = _has_bits_[0];
  if (cached_has_bits & 0x00000001u) {
    total_size += 1 +
      ::google::protobuf::internal::WireFormatLite::BytesSize(
        this->msg());
  }

  int cached_size = ::google::protobuf::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void CMD_TEST::MergeFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:Proto.CMD_TEST)
  GOOGLE_DCHECK_NE(&from, this);
  const CMD_TEST* source =
      ::google::protobuf::DynamicCastToGenerated<CMD_TEST>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:Proto.CMD_TEST)
    ::google::protobuf::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:Proto.CMD_TEST)
    MergeFrom(*source);
  }
}

void CMD_TEST::MergeFrom(const CMD_TEST& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:Proto.CMD_TEST)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::google::protobuf::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.has_msg()) {
    _has_bits_[0] |= 0x00000001u;
    msg_.AssignWithDefault(&::google::protobuf::internal::GetEmptyStringAlreadyInited(), from.msg_);
  }
}

void CMD_TEST::CopyFrom(const ::google::protobuf::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:Proto.CMD_TEST)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void CMD_TEST::CopyFrom(const CMD_TEST& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:Proto.CMD_TEST)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool CMD_TEST::IsInitialized() const {
  return true;
}

void CMD_TEST::Swap(CMD_TEST* other) {
  if (other == this) return;
  InternalSwap(other);
}
void CMD_TEST::InternalSwap(CMD_TEST* other) {
  using std::swap;
  _internal_metadata_.Swap(&other->_internal_metadata_);
  swap(_has_bits_[0], other->_has_bits_[0]);
  msg_.Swap(&other->msg_, &::google::protobuf::internal::GetEmptyStringAlreadyInited(),
    GetArenaNoVirtual());
}

::google::protobuf::Metadata CMD_TEST::GetMetadata() const {
  ::google::protobuf::internal::AssignDescriptors(&::assign_descriptors_table_ProtoBase_2eproto);
  return ::file_level_metadata_ProtoBase_2eproto[kIndexInFileMessages];
}


// @@protoc_insertion_point(namespace_scope)
}  // namespace Proto
namespace google {
namespace protobuf {
template<> PROTOBUF_NOINLINE ::Proto::ProtoBaseCmd* Arena::CreateMaybeMessage< ::Proto::ProtoBaseCmd >(Arena* arena) {
  return Arena::CreateInternal< ::Proto::ProtoBaseCmd >(arena);
}
template<> PROTOBUF_NOINLINE ::Proto::CMD_TEST* Arena::CreateMaybeMessage< ::Proto::CMD_TEST >(Arena* arena) {
  return Arena::CreateInternal< ::Proto::CMD_TEST >(arena);
}
}  // namespace protobuf
}  // namespace google

// @@protoc_insertion_point(global_scope)
#include <google/protobuf/port_undef.inc>
